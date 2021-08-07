using Photon.Pun;
using UnityEngine;

/// <summary>
/// Represents the Player Object. This handles the automatic synchronization of the transforms of the
/// player between the camera and this representation of the player in the space
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class PhotonPlayer : MonoBehaviour
{
    /*
     * Components
     */
    public PhotonView PhotonView { get; private set; }

    /// <summary>
    /// Resource Initialization
    /// </summary>
    private void Awake()
    {
        // Get Components
        PhotonView = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Initialization
    /// </summary>
    private void Start()
    {
        // Ensure that we are a child of the Shared World Anchor
        if (SharedWorldAnchor.Instance != null)
        {
            transform.SetParent(SharedWorldAnchor.Instance.transform, true);
        }

        // We don't care if we are not controlling this
        if (!PhotonView.IsMine)
        {
            // Set name to highlight this is not the player from this device
            gameObject.name = "[Other] " + gameObject.name;
            return;
        }

        // Set name of the player
        gameObject.name = "[Local] " + gameObject.name;
    }

    /// <summary>
    ///  Update Function
    /// </summary>
    private void Update()
    {
        // Set the position and rotation according to the main camera
        if (PhotonView.IsMine)
        {
            transform.position = Camera.main.transform.position;
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}