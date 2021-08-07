/*
 * This script file is used in the Introduction to Gaze and Introduction to Gestures tutorials.
 */
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

// Introduction to Gaze: Exercise 6.7.b
/************************************************************/
// Implement IMixedRealityFocusHandler

// Introduction to Gestures: Exercise 8.1.a
/************************************************************/
// Implement IMixedRealityPointerHandler

// Introduction to Gestures: Exercise 9.1.a
/************************************************************/
// Implement IMixedRealityTouchHandler
/// <summary>
/// Component that represents an interactable poster
/// </summary>
public class ModelPoster : MonoBehaviour
{
    /*
     * Editor Exposed Variables
     */
    [Tooltip("If true, it will activate the HoloLens")]
    [SerializeField]
    private bool isHoloLens;
    [Tooltip("If true, it will activate the car")]
    [SerializeField]
    private bool isCar;
    [Tooltip("If true, it will activate the engine")]
    [SerializeField]
    private bool isEngine;

    /*
     * Member Variables
     */
    private MeshRenderer meshRenderer;

    /// <summary>
    /// Initialization
    /// </summary>
    private void Awake()
    {
        // Get Components
        meshRenderer = GetComponent<MeshRenderer>();

        // Introduction to Gaze: Exercise 6.7.a
        /************************************************************/
        // Set default colour to gray
        
    }

    // Introduction to Gaze: Exercise 6.7.b
    /************************************************************/
    // Implement IMixedRealityFocusHandler's OnFocusEnter()


        // Introduction to Gaze: Exercise 6.7.b.i
        /************************************************************/
        // OnFocusEnter(): 
        // Change colour of the object to white when it is gazed at
   

    // Introduction to Gaze: Exercise 6.7.b
    /************************************************************/
    // Implement IMixedRealityFocusHandler's OnFocusExit()
    

        // Introduction to Gaze: Exercise 6.7.b.ii
        /************************************************************/
        // OnFocusExit():
        // Change colour of the object to gray when it is stopped being gazed at
        
    
    // Introduction to Gestures: Exercise 8.1.a
    /************************************************************/
    // Implement IMixedRealityFocusHandler's OnPointerClicked()
    


        // Introduction to Gestures: Exercise 8.1.b
        /************************************************************/
        // OnPointerClicked():
        // Call ModelLibrary.Instance.ShowModel() to show the correct model
        
    

    // Introduction to Gestures: Exercise 9.1.a
    /************************************************************/
    // Implement IMixedRealityTouchHandler's OnTouchStarted()
    
        // Introduction to Gestures: Exercise 9.1.b
        /************************************************************/
        // OnTouchStarted():
        // Call ModelLibrary.Instance.ShowModel() to show the correct model
        
    
}
