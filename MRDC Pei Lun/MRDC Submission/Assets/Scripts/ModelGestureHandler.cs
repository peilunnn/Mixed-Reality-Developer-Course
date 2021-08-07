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
public class ModelGestureHandler : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealitySpeechHandler
{
    /*
     * Members
     */
    private Vector3 originalPosition;   // The original position of the GameObject at the start of the gesture
    private Vector3 pointerStart;       // The starting position of the gesture pointer
                                        // Introduction to Networked Experiences: Exercise 
                                        /************************************************************/
                                        // Create a member variable to store Photon View objects
    public Material SteveHeadMat;

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
        CoreServices.InputSystem.RegisterHandler<IMixedRealitySpeechHandler>(this);
    }

    /// <summary>
    /// Called when this object is destroyed
    /// </summary>
    private void OnDestroy()
    {
        // Introduction to Voice: Exercise 4.6
        /************************************************************/
        // Unregister this component's IMixedRealitySpeechHandler with the Input System
        CoreServices.InputSystem.UnregisterHandler<IMixedRealitySpeechHandler>(this);
    }


    // Introduction to Gestures: Exercise 10.5
    /************************************************************/
    // Implement IMixedRealityPointerHandler's OnPointerDown()
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        // Introduction to Networked Experiences: Exercise 
        /************************************************************/
        // OnPointerDown():
        // Take control of the object


        // Introduction to Gestures: Exercise 10.6.a
        /************************************************************/
        // OnPointerDown():
        // Save the position of the GameObject to "originalPosition"
        originalPosition = transform.position;
        // Introduction to Gestures: Exercise 10.6.b
        /************************************************************/
        // Save the starting position of the pointer to "pointerStart"
        pointerStart = eventData.Pointer.Position;
    }


    // Introduction to Gestures: Exercise 10.5
    /************************************************************/
    // Implement IMixedRealityPointerHandler's OnPointerDragged()
    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // Introduction to Gestures: Exercise 10.7
        /************************************************************/
        // OnPointerDragged():
        // Reset the position of the GameObject back to “originalPosition”
        // Calculate the distance moved by the pointer
        // Translate the GameObject by the distance moved by the pointer
        var distanceMoved = eventData.Pointer.Position - pointerStart;
        transform.position = originalPosition + distanceMoved;

        if (gameObject.name == "DragMePoster")
        {
            gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }


    // Introduction to Voice: Exercise 4.3
    /************************************************************/
    // Implement IMixedRealitySpeechHandler's OnSpeechKeywordRecognized()

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        // Introduction to Voice: Exercise 4.4
        /************************************************************/
        // OnSpeechKeywordRecognized():
        // If the detected command's keyword is "Show Engine", use "ModelLibrary's" "ShowModel()" to show the engine model
        if (eventData.Command.Keyword == "Show HoloLens")
        {
            ModelLibrary.Instance.ShowModel(true, false, false);
        }
        if (eventData.Command.Keyword == "Show Car")
        {
            ModelLibrary.Instance.ShowModel(false, true, false);
        }
        if (eventData.Command.Keyword == "Show Engine")
        {
            ModelLibrary.Instance.ShowModel(false, false, true);
        }
        //if (eventData.Command.Keyword == "Create Steve")
        //{
            //GameObject SteveHead = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //SteveHead.GetComponent<Renderer>().material = SteveHeadMat;
            //SteveHead.GetComponent<Transform>().position = new Vector3(0, 0.5f, 4);
        //}
        //if (eventData.Command.Keyword == "Destroy Steve")
        //{
        //    Destroy(GameObject.Find("Cube"));
        //}
    }
}
