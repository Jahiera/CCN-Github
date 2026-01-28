using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ======================
    // COLLECTIBLE COUNTING
    // ======================
    private int totalCollectibles;
    private int collectedCount = 0;

    public TMP_Text collectibleText;   // UI counter text

    // ======================
    // INVENTORY SYSTEM
    // ======================
    public bool hasLog = false;          // TRUE if player is holding a log
    public GameObject heldLog;           // The actual log GameObject
    public Transform inventorySlot;      // Top-left UI position

    void Start()
    {
        // Count collectibles automatically
        totalCollectibles = GameObject
            .FindObjectsByType<Collectable>(FindObjectsSortMode.None)
            .Length;

        collectedCount = 0;

        // Initialize UI text
        if (collectibleText != null)
        {
            collectibleText.text = "Boost Fire 0/" + totalCollectibles;
        }
    }

    // ======================
    // CALLED WHEN PLAYER PICKS UP A LOG
    // ======================
    public void CollectItem(GameObject log)
    {
        if (hasLog) return; // player can only hold ONE log

        hasLog = true;
        heldLog = log;

        // Move log to inventory slot
        log.transform.SetParent(inventorySlot);
        log.transform.localPosition = Vector3.zero;

        // Disable physics and collision
        Collider2D col = log.GetComponent<Collider2D>();
        if (col) col.enabled = false;

        Rigidbody2D rb = log.GetComponent<Rigidbody2D>();
        if (rb) rb.simulated = false;
    }

    // ======================
    // CALLED WHEN PLAYER PLACES LOG IN CAMPFIRE
    // ======================
    public void PlaceLog()
    {
        if (!hasLog || heldLog == null) return;

        // Destroy the log AFTER placing
        Destroy(heldLog);
        heldLog = null;
        hasLog = false;

        // Increase count ONLY here
        collectedCount++;

        if (collectibleText != null)
        {
            collectibleText.text =
                "Collected " + collectedCount + "/" + totalCollectibles;
        }

        // Load next scene when all placed
        if (collectedCount >= totalCollectibles)
        {
            SceneManager.LoadScene("ComingSoon");
        }
    }
}
