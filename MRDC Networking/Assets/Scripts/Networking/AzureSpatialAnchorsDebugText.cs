using TMPro;
using UnityEngine;

/// <summary>
/// Helper Component to display debug information from the Azure Spatial Anchors Manager
/// on a TextMeshPro object
/// </summary>
[RequireComponent(typeof(TextMeshPro))]
public class AzureSpatialAnchorsDebugText : MonoBehaviour
{
    /*
     * Members
     */
    // Reference to the Azure Spatial Anchors Manager
    private AzureSpatialAnchorsManager asaManager;

    /*
     * Components
     */ 
    public TextMeshPro TextMeshPro { get; private set; }


    /// <summary>
    /// Initialization
    /// </summary>
    private void Awake()
    {
        // Get Components
        TextMeshPro = GetComponent<TextMeshPro>();

        // Get the ASA Manager object to do checking
        asaManager = FindObjectOfType<AzureSpatialAnchorsManager>();
    }

    /// <summary>
    /// Update Function
    /// </summary>
    void Update()
    {
        // Deactivate if no ASA Manager
        if (asaManager == null)
        {
            gameObject.SetActive(false);
        }

        // Show the appropriate message
        // -- Editor Simulation
        if (Application.isEditor)
        {
            TextMeshPro.text = "Not Anchored\nSimulation in Editor";
        }
        // -- On Device
        else
        {
            if (asaManager.AnchorWatcher != null)
            {
                TextMeshPro.text = "Locating Anchor...";
            }
            else if (asaManager.CloudSpatialAnchorId == "")
            {
                TextMeshPro.text = "Not Anchored";
            }
            else
            {
                TextMeshPro.text = "Anchored\n" + asaManager.CloudSpatialAnchorId;
            }
        }
    }
}
