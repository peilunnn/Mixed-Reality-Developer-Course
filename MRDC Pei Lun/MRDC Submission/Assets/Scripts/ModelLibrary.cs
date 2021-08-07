/*
 * This script file is used in the Introduction to Networking tutorial.
 * Additionally, it also contains some functions that are used in the Introduction to Gaze
 * and Gestures tutorial.
 */
using UnityEngine;

/// <summary>
/// Component that contains helpful functions for working with the set of models in the app
/// </summary>
// Introduction to Networked Experiences: Exercise 14.9
/************************************************************/
// Implement IPunObservable
public class ModelLibrary : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static ModelLibrary Instance { get; private set; }
    #endregion

    /*
     * Editor Exposed Variables
     */
    [Header("Configuration")]
    [Tooltip("The reference to the HoloLens Game Object")]
    [SerializeField]
    private GameObject hololensModel;
    [Header("Configuration")]
    [Tooltip("The reference to the HoloLens Game Object")]
    [SerializeField]
    private GameObject carModel;
    [Header("Configuration")]
    [Tooltip("The reference to the HoloLens Game Object")]
    [SerializeField]
    private GameObject engineModel;

    /*
     * Member Variables
     */
    // Original Values
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    // Introduction to Networked Experiences: Exercise 14.7
    /************************************************************/
    // Add a variable to store a reference to the Photon View component
    

    /// <summary>
    /// Resource Initialization
    /// </summary>
    private void Awake()
    {
        #region Singleton Initialization
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than 1 Model Library instance detected! Deleting this new one.");
            Destroy(this);
            return;
        }
        #endregion

        // Error Checks
        if (hololensModel == null)
        {
            Debug.LogError("ModelLibrary: HoloLens model not set.");
        }
        if (carModel == null)
        {
            Debug.LogError("ModelLibrary: Car model not set.");
        }
        if (engineModel == null)
        {
            Debug.LogError("ModelLibrary: Engine model not set.");
        }

        // Introduction to Networked Experiences: Exercise 14.8
        /************************************************************/
        // Get a reference to the Photon View component
        

        // Show the hololens at the start
        ShowModel(true, false, false);

        // Save original transforms
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    /// <summary>
    /// Deinitialization
    /// </summary>
    private void OnDestroy()
    {
        #region Singleton Deinitialization
        if (Instance == this)
        {
            Instance = null;
        }
        #endregion
    }

    /// <summary>
    /// Function to hide all models
    /// </summary>
    public void HideAllModels()
    {
        // Introduction to Networked Experiences: Exercise 14.11
        /************************************************************/
        // Take ownership of this GameObject across the network
        
 

        // Hide all models
        hololensModel.SetActive(false);
        carModel.SetActive(false);
        engineModel.SetActive(false);
    }

    /// <summary>
    /// Function to show the model based on the parameters provided 
    /// </summary>
    /// <param name="hololens">Whether to show the hololens model</param>
    /// <param name="car">Whether to show the car model</param>
    /// <param name="engine">Whether to show the engine model</param>
    public void ShowModel(bool hololens, bool car, bool engine)
    {
        HideAllModels();

        if (hololens)
        {
            hololensModel.SetActive(true);
        }
        else if (car)
        {
            carModel.SetActive(true);
        }
        else if (engine)
        {
            engineModel.SetActive(true);
        }
    }
 
    /// Function to reset the transforms of this object
    /// </summary>
    public void ResetTransforms()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
    }

    // Introduction to Networked Experiences: Exercise 14.10
    /************************************************************/
    // Implement IPunObservable to synchronize showing the correct model
    

        // Introduction to Networked Experiences: Exercise 14.10.a
        /************************************************************/
        // OnPhotonSerializeView():
        // Check if the stream is writing
        

            // Introduction to Networked Experiences: Exercise 14.10.a
            /************************************************************/
            // OnPhotonSerializeView():
            // If it is, send the active states of each model in order
            


        
        // Introduction to Networked Experiences: Exercise 14.10.b
        /************************************************************/
        // OnPhotonSerializeView():
        // Otherwise, check if the stream is reading
        
        
            // Introduction to Networked Experiences: Exercise 14.10.b
            /************************************************************/
            // OnPhotonSerializeView():
            // If it is, receive the active states of each model in order 
            // and use it to update the state of the model
            
            
            
        
    
}
