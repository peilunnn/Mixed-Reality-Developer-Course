using UnityEngine;

/// <summary>
/// Singleton object that holds a reference to the Shared Anchor Object
/// </summary>
public class SharedWorldAnchor : MonoBehaviour
{
    /*
     * Statics
     */
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static SharedWorldAnchor Instance { get; private set; }

    #region MonoBehaviour
    /// <summary>
    /// Resource Initialization
    /// </summary>
    private void Awake()
    {
        // Check if this is the only instance
        if (Instance != null)
        {
            // This is a secondary instance, delete it
            Destroy(gameObject);

            return;
        }

        // Set instance
        Instance = this;
    }

    /// <summary>
    /// Deinitialization
    /// </summary>
    private void OnDestroy()
    {
        // Unset the local instance
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion
}