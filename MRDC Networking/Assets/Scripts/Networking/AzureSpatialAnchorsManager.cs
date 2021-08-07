using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.WSA;

/// <summary>
/// Controls and interface for working with the Azure Spatial Anchors service.
/// Based on the code at
/// https://docs.microsoft.com/en-us/azure/spatial-anchors/tutorials/tutorial-new-unity-hololens-app?tabs=UnityPackage#get-the-azure-spatial-anchors-sdk 
/// 
/// However, we have cleaned up the code and added some features:
/// - Extracted anchor creation and location code into 2 easy to use functions
/// - Removed testing code from the script so that you can use it with your own code
/// - Allow for a Unity Editor hosted session to connect to a HoloLens and vice-versa
/// - Added automatic 1-day expiration of anchors to save server costs
/// </summary>
public class AzureSpatialAnchorsManager : MonoBehaviour
{
    /*
     * Constants
     */
    /// <summary>
    /// The fake cloud spatial anchor ID used when running on the editor
    /// </summary>
    public const string FakeCloudSpatialAnchorId = "SAMPLE-ANCHOR-ID-ON-THE-UNITY-EDITOR";

    /*
     * Serializable
     */
    [Tooltip("The domain for the ASA service. Get from the Azure Portal.")]
    [SerializeField]
    private string spatialAnchorsDomain;
    [Tooltip("The account ID for the ASA service. Get from the Azure Portal.")]
    [SerializeField]
    private string spatialAnchorsAccountId;
    [Tooltip("The account key for the ASA service. Get from the Azure Portal.")]
    [SerializeField]
    private string spatialAnchorsAccountKey;

    /*
     * Members
     */
    // The CloudSpatialAnchor that we either 1) placed and are saving or 2) just located.
    private CloudSpatialAnchor currentCloudAnchor;
    // The current status of the spatial mapping scan for generating an anchor. 1.0 is the minimum good value.
    private float recommendedForCreate = 0.0f;

    /*
     * Properties
     */
    /// <summary>
    /// The ASA Management session
    /// </summary>
    public CloudSpatialAnchorSession CloudSpatialAnchorSession;
    /// <summary>
    /// The ID of the Azure Spatial Anchor that has been created
    /// </summary>
    public string CloudSpatialAnchorId { get; private set; }
    /// <summary>
    /// Watcher object for locating anchors
    /// </summary>
    public CloudSpatialAnchorWatcher AnchorWatcher { get; private set; }

    /// <summary>
    /// Initalization
    /// </summary>
    private void Start()
    {
        initializeSession();
    }

    /// <summary>
    /// Initializes a new CloudSpatialAnchorSession
    /// </summary>
    private void initializeSession()
    {
        Debug.Log("ASA Info: Initializing a CloudSpatialAnchorSession.");

        /*
         * Error Checks
         */ 
        if (string.IsNullOrEmpty(spatialAnchorsAccountId))
        {
            Debug.LogError("No account id set.");
            return;
        }

        if (string.IsNullOrEmpty(spatialAnchorsAccountKey))
        {
            Debug.LogError("No account key set.");
            return;
        }

        // Can't simulate on Unity Editor so we are just going to skip it
        if (Application.isEditor)
        {
            Debug.Log("ASA: Simulating initialization process in editor.");
            return;
        }

        /*
         * Initialization
         */
        // Create a session to interact with the ASA service
        CloudSpatialAnchorSession = new CloudSpatialAnchorSession();

        // Credentials
        CloudSpatialAnchorSession.Configuration.AccountId = spatialAnchorsAccountId.Trim();
        CloudSpatialAnchorSession.Configuration.AccountKey = spatialAnchorsAccountKey.Trim();
        CloudSpatialAnchorSession.Configuration.AccountDomain = spatialAnchorsDomain.Trim();

        // Debug Logs
        CloudSpatialAnchorSession.LogLevel = SessionLogLevel.All;

        // Logging Callbacks
        CloudSpatialAnchorSession.Error += CloudSpatialAnchorSession_Error;
        CloudSpatialAnchorSession.OnLogDebug += CloudSpatialAnchorSession_OnLogDebug;

        // Anchor Creation Callback
        CloudSpatialAnchorSession.SessionUpdated += CloudSpatialAnchorSession_SessionUpdated; 

        // Anchor Location Callbacks
        CloudSpatialAnchorSession.AnchorLocated += CloudSpatialAnchorSession_AnchorLocated;
        CloudSpatialAnchorSession.LocateAnchorsCompleted += CloudSpatialAnchorSession_LocateAnchorsCompleted;

        // Start the Session
        CloudSpatialAnchorSession.Start();

        Debug.Log("ASA Info: Session was initialized.");
    }

    /// <summary>
    /// Creates an anchor at the Shared World Anchor's position and orientation
    /// </summary>
    /// <returns>The asynchronous Task that for this asynchronous function that will return the status of the anchor creation.</returns>
    public async Task<bool> CreateAnchorAsync()
    {
        // Set the LocalAnchor property of the CloudSpatialAnchor to the WorldAnchor component of our white sphere.
        WorldAnchor worldAnchor = SharedWorldAnchor.Instance.gameObject.AddComponent<WorldAnchor>();

        // In Editor, don't bother
        if (Application.isEditor)
        {
            // Simulate that we have found an anchor
            CloudSpatialAnchorId = FakeCloudSpatialAnchorId;
            return true;
        }
        
        // Create the CloudSpatialAnchor.
        currentCloudAnchor = new CloudSpatialAnchor();
        currentCloudAnchor.Expiration = DateTimeOffset.Now.AddDays(1);

        // Save the CloudSpatialAnchor to the cloud.
        currentCloudAnchor.LocalAnchor = worldAnchor.GetNativeSpatialAnchorPtr();

        // Wait for enough data about the environment.
        while (recommendedForCreate < 1.0F)
        {
            await Task.Delay(330);
        }

        bool success = false;
        try
        {
            await CloudSpatialAnchorSession.CreateAnchorAsync(currentCloudAnchor);
            success = currentCloudAnchor != null;

            if (success)
            {

                // Record the identifier to locate.
                CloudSpatialAnchorId = currentCloudAnchor.Identifier;

                Debug.Log("ASA Info: Saved anchor to Azure Spatial Anchors! Identifier: " + CloudSpatialAnchorId);
            }
            else
            {
                Debug.LogError("ASA Error: Failed to save, but no exception was thrown.");
            }

            return success;
        }
        catch (Exception ex)
        {
            Debug.LogError("ASA Error: " + ex.Message);
        }

        return false;
    }

    /// <summary>
    /// Begins the anchor location procedure using the ASA Anchor Watchers
    /// </summary>
    /// <param name="anchorId">The ID of the anchor to locate.</param>
    public void LocateAnchor(string anchorId)
    {
        // Ignore if it is a fake anchor
        if (anchorId == FakeCloudSpatialAnchorId)
        {
            return;
        }

        if (AnchorWatcher != null)
        {
            Debug.LogError("ASA Info: A watcher is already running.");
            return;
        }

        AnchorLocateCriteria criteria = new AnchorLocateCriteria();
        criteria.Identifiers = new string[] { anchorId };
        AnchorWatcher = CloudSpatialAnchorSession.CreateWatcher(criteria);
    }

    /// <summary>
    /// Callback function that is executed when an anchor is located.
    /// </summary>
    private void CloudSpatialAnchorSession_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        switch (args.Status)
        {
            case LocateAnchorStatus.Located:
                Debug.Log("ASA Info: Anchor located! Identifier: " + args.Identifier);
                UnityDispatcher.InvokeOnAppThread(() =>
                {
                    // Create a green sphere.
                    var worldAnchor = SharedWorldAnchor.Instance.gameObject.AddComponent<WorldAnchor>();

                    // Get the WorldAnchor from the CloudSpatialAnchor and use it to position the sphere.
                    worldAnchor.SetNativeSpatialAnchorPtr(args.Anchor.LocalAnchor);
                });
                break;
            case LocateAnchorStatus.AlreadyTracked:
                Debug.Log("ASA Info: Anchor already tracked. Identifier: " + args.Identifier);
                break;
            case LocateAnchorStatus.NotLocated:
                Debug.Log("ASA Info: Anchor not located. Identifier: " + args.Identifier);
                break;
            case LocateAnchorStatus.NotLocatedAnchorDoesNotExist:
                Debug.LogError("ASA Error: Anchor not located does not exist. Identifier: " + args.Identifier);
                break;
        }

        // Clear Anchor Watcher
        AnchorWatcher = null;
    }

    /// <summary>
    /// Callback function that is executed when an anchor location procedure is completed.
    /// </summary>
    private void CloudSpatialAnchorSession_LocateAnchorsCompleted(object sender, LocateAnchorsCompletedEventArgs args)
    {
        Debug.Log("ASA Info: Locate anchors completed. Watcher identifier: " + args.Watcher.Identifier);
    }

    /// <summary>
    /// Callback function that is executed when an ASA error is thrown
    /// </summary>
    private void CloudSpatialAnchorSession_Error(object sender, SessionErrorEventArgs args)
    {
        Debug.LogError("ASA Error: " + args.ErrorMessage);
    }

    /// <summary>
    /// Callback function that is executed when an ASA debug message is logged
    /// </summary>
    private void CloudSpatialAnchorSession_OnLogDebug(object sender, OnLogDebugEventArgs args)
    {
        Debug.Log("ASA Log: " + args.Message);
        System.Diagnostics.Debug.WriteLine("ASA Log: " + args.Message);
    }

    /// <summary>
    /// Callback function that is executed when the anchor session information is updated
    /// </summary>
    private void CloudSpatialAnchorSession_SessionUpdated(object sender, SessionUpdatedEventArgs args)
    {
        Debug.Log("ASA Log: recommendedForCreate: " + args.Status.RecommendedForCreateProgress);
        recommendedForCreate = args.Status.RecommendedForCreateProgress;
    }
}
