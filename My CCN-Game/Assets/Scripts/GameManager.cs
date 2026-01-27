using UnityEngine;
using UnityEngine.SceneManagement; // to load other scenes
using TMPro; // for the text UI



public class GameManager : MonoBehaviour
{
    // public int totalCollectibles = 5;   // how many collectibles in the level
    private int totalCollectibles;
    private int collectedCount = 0;
    public TMP_Text collectibleText;  // UI Text

    void Start()
    {
        // Automatically count all collectibles in the scene
        totalCollectibles = GameObject.FindObjectsByType<Collectable>(FindObjectsSortMode.None).Length;
        collectedCount = 0;

        // Initialize UI
        if (collectibleText != null)
        {
            collectibleText.text = "Collected 0/" + totalCollectibles;
        }

        Debug.Log("Total Collectibles in level: " + totalCollectibles);
    }
    
    // Call this when a collectable is collected
    public void CollectItem()
    {
        collectedCount++;
        
        if (collectibleText != null)
        {
            collectibleText.text = "Collected " + collectedCount + "/" + totalCollectibles;
        }

        Debug.Log("Collected: " + collectedCount);

        if (collectedCount >= totalCollectibles)
        {
            // All collected! Go to ComingSoon scene
            SceneManager.LoadScene("ComingSoon");
        }
    }
}

