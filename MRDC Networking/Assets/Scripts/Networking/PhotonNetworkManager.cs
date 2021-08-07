/*
 * This script file is used in the Introduction to Networking tutorial.
 */
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
// The below namespaces are used in this script to get the device name on the UWP platform
#if !UNITY_EDITOR && UNITY_WSA
using Windows.Networking;
using Windows.Networking.Connectivity;
#endif

/// <summary>
/// Handles the Photon networking portions of the app
/// - Joining
/// - Hosting
/// - Creating/Loading Spatial Anchors
/// - Pre and Post Join procedures
/// </summary>
public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    /*
     * Serializable
     */
    [Header("Resources")]
    [Tooltip("List of session controls that will have the session info on them.")]
    [SerializeField]
    private PhotonSessionButton[] roomButtons;
    [Tooltip("The network menu GameObject.")]
    [SerializeField]
    private GameObject networkMenu;
    [Tooltip("The networked Player Prefab.")]
    [SerializeField]
    private PhotonPlayer playerObject;

    /*
     * Components
     */
    public AzureSpatialAnchorsManager ASAManager { get; private set; }

    /// <summary>
    /// Initialization
    /// </summary>
    void Start()
    {
        // Get Components
        ASAManager = GetComponent<AzureSpatialAnchorsManager>();

        // Introduction to Networked Experiences: Exercise 9.5.a
        /************************************************************/
        // Connect to the Photon master server at the start
        PhotonNetwork.ConnectUsingSettings();

        // Introduction to Networked Experiences: Exercise 12.3
        /************************************************************/
        // Deactivate content of our application at the start before we are in a session
        SharedWorldAnchor.Instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// Function to be executed by the button to host the session
    /// </summary>
    public void Host()
    {
        // Introduction to Networked Experiences: Exercise 10.3
        /************************************************************/
        // Create the room and use GetDeviceName() to generate an appropriate name for the room
        PhotonNetwork.CreateRoom(GetDeviceName());
    }

    /// <summary>
    /// Function that is automatically called when the user is connected to the master server
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("[Photon Network Manager] Successfully connected to the master server!");

        // Introduction to Networked Experiences: Exercise 9.5.b
        /************************************************************/
        // Join the lobby after connecting
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Function that is automatically called when the user is connected to a lobby
    /// </summary>
    public override void OnJoinedLobby()
    {
        Debug.Log("[Photon Network Manager] Successfully connected to a lobby!");
    }

    /// <summary>
    /// Function that is automatically called whenever the room list is updated.
    /// We will use this to update our UI that shows what rooms are available
    /// </summary>
    /// <param name="updatedroomList">A list of information about available rooms</param>
    public override void OnRoomListUpdate(List<RoomInfo> updatedroomList)
    {
        // Ensure only available rooms are listed
        for (int i = 0; i < roomButtons.Count(); ++i)
        {
            if (i >= updatedroomList.Count())
            {
                roomButtons[i].SetSessionInfo(null);
            }
            else
            {
                roomButtons[i].SetSessionInfo(updatedroomList[i]);
            }
        }

    }

    /// <summary>
    /// Function that is automatically called upon joining a room
    /// </summary>
    public override async void OnJoinedRoom()
    {
        // Hide the network menu since we don't need it anymore
        networkMenu.SetActive(false);

        // Introduction to Networked Experiences: Exercise 12.4
        /************************************************************/
        // Show the content since we are now in a room
        SharedWorldAnchor.Instance.gameObject.SetActive(true);

        // Introduction to Networked Experiences: Exercise 12.5
        /************************************************************/
        // Create our player object
        PhotonNetwork.Instantiate(playerObject.name, Camera.main.transform.position, Camera.main.transform.rotation);

        // Check if we are the server
        if (PhotonNetwork.IsMasterClient)
        {
            // Introduction to Networked Experiences: Exercise 13.1-2
            /************************************************************/
            // Server needs to create the anchor and check if we are able to do so successfully
            if (await ASAManager.CreateAnchorAsync())
            {
                // Introduction to Networked Experiences: Exercise 13.3
                /************************************************************/
                // Store the ID in custom properties so that clients can grab the ID
                PhotonNetwork.CurrentRoom.CustomProperties.Add("SpatialId", ASAManager.CloudSpatialAnchorId);
            }
        }
        // Clients need to collect an anchor
        else
        {
            // Try to query for the anchor id
            string anchorId = "";
            while (true)
            {
                // Introduction to Networked Experiences: Exercise 13.4
                /************************************************************/
                // Grab the stored anchor ID in custom properties
                anchorId = (string)PhotonNetwork.CurrentRoom.CustomProperties["SpatialId"];

                // No anchor found, wait awhile then search again
                if (string.IsNullOrEmpty(anchorId))
                {
                    await Task.Delay(330);
                }
                else
                {
                    // Found
                    break;
                }
            }

            // Introduction to Networked Experiences: Exercise 13.5
            /************************************************************/
            // Clients need to locate the anchor once we are able to get the anchor ID
            ASAManager.LocateAnchor(anchorId);
        }
    }

    /// <summary>
    /// Gets the local computer name if it can.
    /// </summary>
    /// <returns>The name of the computer</returns>
    public string GetDeviceName()
    {
#if !UNITY_EDITOR && UNITY_WSA
        foreach (HostName hostName in NetworkInformation.GetHostNames())
        {
            if (hostName.Type == HostNameType.DomainName)
            {
                return hostName.DisplayName;
            }
        }
        return "No Name";
#elif UNITY_ANDROID || UNITY_IOS
        // Check if we are able the get device name
        if (SystemInfo.deviceName == "<unknown>")
        {
            // Device model instead if we aren't able to get it
            return SystemInfo.deviceModel;
        }
            
        // If we are, then return the device name
        return SystemInfo.deviceName;
#else
        return System.Environment.ExpandEnvironmentVariables("%ComputerName%");
#endif
    }
}
