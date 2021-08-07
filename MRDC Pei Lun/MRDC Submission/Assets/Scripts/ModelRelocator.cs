using UnityEngine;

/// <summary>
/// Script that implements an air tap gesture that will teleport the Model Library to it's position and orientation
/// </summary>
public class ModelRelocator : MonoBehaviour
{
    /*
     * Serializable
     */
    [Tooltip("The vertical offset to position the model such that it is positioned centrally")]
    [SerializeField]
    private float verticalOffset = 0.05f;

    /// <summary>
    /// Handles air tap events
    /// </summary>
    public void RelocateButtonHere()
    {
        // Teleport the Model Library here
        ModelLibrary.Instance.transform.position = transform.position;
        ModelLibrary.Instance.transform.rotation = transform.rotation;

        // Add offset
        ModelLibrary.Instance.transform.Translate(0.0f, verticalOffset, 0.0f);
    }
}