/*
 * This script file is used in the Introduction to Gestures tutorial.
 */
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

// Introduction to Gestures: Exercise 10.5
/************************************************************/
// Implement IMixedRealityPointerHandler

// Introduction to Voice: Exercise 4.3
/************************************************************/
// Implement IMixedRealitySpeechHandler
/// <summary>
/// Component that is responsible for providing gesture support to the Model
/// </summary>
public class ModelGestureHandler : MonoBehaviour
{
    /*
     * Members
     */
    private Vector3 originalPosition;   // The original position of the GameObject at the start of the gesture
    private Vector3 pointerStart;       // The starting position of the gesture pointer
    // Introduction to Networked Experiences: Exercise 
    /************************************************************/
    // Create a member variable to store Photon View objects
    

    /// <summary>
    /// Called when this component becomes active for the first time
    /// </summary>
    private void Start()
    {
        // Introduction to Networked Experiences: Exercise 
        /************************************************************/
        // Get the Photon View component
        

        // Introduction to Voice: Exercise 4.5
        /************************************************************/
        // Register this component's IMixedRealitySpeechHandler with the Input System
        
    }

    /// <summary>
    /// Called when this object is destroyed
    /// </summary>
    private void OnDestroy()
    {
        // Introduction to Voice: Exercise 4.6
        /************************************************************/
        // Unregister this component's IMixedRealitySpeechHandler with the Input System
        
    }


    // Introduction to Gestures: Exercise 10.5
    /************************************************************/
    // Implement IMixedRealityPointerHandler's OnPointerDown()
    

        // Introduction to Networked Experiences: Exercise 
        /************************************************************/
        // OnPointerDown():
        // Take control of the object
        

        // Introduction to Gestures: Exercise 10.6.a
        /************************************************************/
        // OnPointerDown():
        // Save the position of the GameObject to "originalPosition"
        
        // Introduction to Gestures: Exercise 10.6.b
        /************************************************************/
        // Save the starting position of the pointer to "pointerStart"
        
    


    // Introduction to Gestures: Exercise 10.5
    /************************************************************/
    // Implement IMixedRealityPointerHandler's OnPointerDragged()
    

        // Introduction to Gestures: Exercise 10.7
        /************************************************************/
        // OnPointerDragged():
        // Reset the position of the GameObject back to “originalPosition”
        // Calculate the distance moved by the pointer
        // Translate the GameObject by the distance moved by the pointer
        
        
    

    // Introduction to Voice: Exercise 4.3
    /************************************************************/
    // Implement IMixedRealitySpeechHandler's OnSpeechKeywordRecognized()

    

        // Introduction to Voice: Exercise 4.4
        /************************************************************/
        // OnSpeechKeywordRecognized():
        // If the detected command's keyword is "Show Engine", use "ModelLibrary's" "ShowModel()" to show the engine model
        



    

}
