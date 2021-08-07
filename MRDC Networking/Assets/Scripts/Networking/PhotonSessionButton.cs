/*
 * This script file is used in the Introduction to Networking tutorial.
 */
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents a button on the scrolling session list
/// </summary>
public class PhotonSessionButton : MonoBehaviour
{
    /*
     * Members
     */
    protected RoomInfo currentRoomInfo;                                   // Information about the session attached to this button

    /*
     * Components
     */
    public TextMeshPro TextMeshPro { get; private set; }

    /// <summary>
    /// Initialization
    /// </summary>
    protected void Awake()
    {
        TextMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    /// <summary>
    /// Sets the session information associated with this button
    /// </summary>
    /// <param name="sessionInfo">The session info</param>
    public void SetSessionInfo(RoomInfo roomInfo)
    {
        // Update the button of the new session it is representing
        currentRoomInfo = roomInfo;
        if (currentRoomInfo == null)
        {
            TextMeshPro.text = "Searching...";
        }
        else
        {
            // Update the name of the session that this button represents
            if (TextMeshPro != null)
            {
                TextMeshPro.text = currentRoomInfo.Name;
            }
        }
    }

    /// <summary>
    /// Function to join a session
    /// </summary>
    public void JoinRoom()
    {
        // Introduction to Networked Experiences: Exercise 11.4
        /************************************************************/
        // Join the lobby after connecting
        PhotonNetwork.JoinRoom(currentRoomInfo.Name);
    }
}