using Photon.Pun;
using UnityEngine;

/// <summary>
/// Component that provides an easy to use "TakeOwnership()" to take over ownership of a Photon View
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class PhotonTakeoverHelper : MonoBehaviour
{
    /*
     * Components
     */
    private PhotonView photonView;

    /// <summary>
    /// Resource Initialization
    /// </summary>
    private void Awake()
    {
        // Get the Photon View component
        photonView = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Function to help take over ownership of a Photon View
    /// </summary>
    public void TakeOwnership()
    {
        // Take Ownership
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}