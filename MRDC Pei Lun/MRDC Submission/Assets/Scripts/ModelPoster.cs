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
public class ModelPoster : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler, IMixedRealityTouchHandler
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
        meshRenderer.material.color = Color.gray;
    }

    // Introduction to Gaze: Exercise 6.7.b
    /************************************************************/
    // Implement IMixedRealityFocusHandler's OnFocusEnter()
    public void OnFocusEnter(FocusEventData eventData)
    {
        // Introduction to Gaze: Exercise 6.7.b.i
        /************************************************************/
        // OnFocusEnter(): 
        // Change colour of the object to white when it is gazed at
        meshRenderer.material.color = Color.white;
    }

    // Introduction to Gaze: Exercise 6.7.b
    /************************************************************/
    // Implement IMixedRealityFocusHandler's OnFocusExit()
    public void OnFocusExit(FocusEventData eventData)
    {
        // Introduction to Gaze: Exercise 6.7.b.ii
        /************************************************************/
        // OnFocusExit():
        // Change colour of the object to gray when it is stopped being gazed at
        meshRenderer.material.color = Color.gray;
    }

    // Introduction to Gestures: Exercise 8.1.a
    /************************************************************/
    // Implement IMixedRealityFocusHandler's OnPointerClicked()
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Introduction to Gestures: Exercise 8.1.b
        /************************************************************/
        // OnPointerClicked():
        // Call ModelLibrary.Instance.ShowModel() to show the correct model
        ModelLibrary.Instance.ShowModel(isHoloLens, isCar, isEngine);
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    // Introduction to Gaze: Exercise 9.1.a
    /************************************************************/
    // Implement IMixedRealityTouchHandler's OnTouchStarted()
    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        // Introduction to Gestures: Exercise 9.1.b
        /************************************************************/
        // OnTouchStarted():
        // Call ModelLibrary.Instance.ShowModel() to show the correct model
        ModelLibrary.Instance.ShowModel(isHoloLens, isCar, isEngine);
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
    }
}
