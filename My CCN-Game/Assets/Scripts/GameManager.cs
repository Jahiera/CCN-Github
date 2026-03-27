using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // COLLECTIBLE COUNTING
    private int totalCollectibles;
    private int collectedCount = 0;
    public int CollectedCount => collectedCount;

    public TMP_Text collectibleText;   // UI counter text
    public GameObject logIcon; // UI icon for inventory

    
    // INVENTORY SYSTEM
    public bool hasLog = false;          // TRUE if player is holding a log
    public GameObject heldLog;           // The actual log GameObject
    public Transform inventorySlot;      // Top-left UI position
    
    // Vignette
    public Image vignetteImage;
    private float currentAlpha = 1f;    // starts fully opaque
    private float targetAlpha = 1f;     
    private float fadeSpeed = 2f; // adjust this value for smoothness 
    
    // CampfireGlow
    public SpriteRenderer campfireGlow;  // Drag the glow circle sprite here
    private float currentGlowAlpha = 0f; // starts invisible
    private float targetGlowAlpha = 0f;
    private float glowFadeSpeed = 2f;
    private float maxGlowAlpha = 0.5f;   // max brightness of the glow
    private bool glowActive = false; // only true after first log
    
    
    // Sound for placing log
    public AudioClip placeLogSound; 
    private AudioSource audioSource;
    
    void Start()
    {
        // Count collectibles automatically
        totalCollectibles = GameObject.FindObjectsByType<Collectable>(FindObjectsSortMode.None).Length;
        collectedCount = 0;

        // Initialize UI text
        if (collectibleText != null)
        {
            collectibleText.text = "Boost Fire 0/" + totalCollectibles;
        }
        
        // Set vignette fully opaque at start
        if (vignetteImage != null)
        {
            Color c = vignetteImage.color;
            c.a = 1f;
            vignetteImage.color = c;
            currentAlpha = 1f;
            targetAlpha = 1f;
        }
        
        // Make sure glow is invisible at start
        if (campfireGlow != null)
        {
            Color glowColor = campfireGlow.color;
            glowColor.a = 0f;
            campfireGlow.color = glowColor;
            currentGlowAlpha = 0f;
            targetGlowAlpha = 0f;
        }
        
        // Setup AudioSource for log placement
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        
    }
    
    
    void Update()
    {
        if (vignetteImage != null)
        {
            // Smooth fade towards targetAlpha
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);
            Color c = vignetteImage.color;
            c.a = currentAlpha;
            vignetteImage.color = c;
        }
        
        // Smoothly fade/pulse glow
        if (campfireGlow != null && glowActive)
        {
            // Smooth lerp towards target alpha
            currentGlowAlpha = Mathf.Lerp(currentGlowAlpha, targetGlowAlpha, Time.deltaTime * glowFadeSpeed);

            // Add a slow flicker/pulse
            float flicker = Mathf.Sin(Time.time * 3f) * 0.03f; // slower frequency
            Color glowColor = campfireGlow.color;
            glowColor.a = Mathf.Clamp(currentGlowAlpha + flicker, 0f, maxGlowAlpha);
            campfireGlow.color = glowColor;
        }
        
    }      
    
    // CALLED WHEN PLAYER PICKS UP A LOG
    public void CollectItem(GameObject log)
    {
        if (hasLog) return; // player can only hold ONE log

        hasLog = true;
        heldLog = log;
        
        // Show UI log icon
        if (logIcon != null)
        {
            logIcon.SetActive(true);
        }

        // Disable world log
        log.SetActive(false);
        
        // make camp appear
        log.transform.localScale = Vector3.one * 0.5f; // adjust if too big/small
        log.transform.rotation = Quaternion.identity;

        // Disable physics and collision
        Collider2D col = log.GetComponent<Collider2D>();
        if (col) col.enabled = false;

        Rigidbody2D rb = log.GetComponent<Rigidbody2D>();
        if (rb) rb.simulated = false;
    }
    
    // CALLED WHEN PLAYER PLACES LOG IN CAMPFIRE
    public void PlaceLog()
    {
        if (!hasLog || heldLog == null) return;

        // Destroy the log AFTER placing
       
        heldLog = null;
        hasLog = false;
        
        // Hide UI icon
        if (logIcon != null)
        {
            logIcon.SetActive(false);
        }

        // Increase count ONLY here
        collectedCount++;

        if (collectibleText != null)
        {
            collectibleText.text = "Fire Boosted " + collectedCount + "/" + totalCollectibles;
        }
        
        // Update vignette target alpha
        if (vignetteImage != null)
        {
            // Each log fades the vignette a bit
            targetAlpha = Mathf.Clamp01(1f - ((float)collectedCount / totalCollectibles));
        }
        
        //Update campfire glow
        // Update campfire glow
        if (campfireGlow != null && collectedCount > 0)
        {
            glowActive = true; // start glow pulsing

            // Starts faint, scales with logs
            float minGlow = 0.005f; // faint start
            float addedGlow = 0.25f * ((float)collectedCount / totalCollectibles); 
            targetGlowAlpha = Mathf.Clamp(minGlow + addedGlow, 0f, maxGlowAlpha);
        }
        
        //Log Placed Sound
        if (audioSource != null && placeLogSound != null)
        {
            audioSource.PlayOneShot(placeLogSound);
        }

        // Load next scene when all logs placed
        if (collectedCount >= totalCollectibles)
        {
            PlayerPrefs.SetInt("Level1Completed", 1);
            PlayerPrefs.Save(); // game registers when level 1 is completed in order to unlock ticket 2 
            
            SceneManager.LoadScene("MainMenu"); // goes back to main menu when level 1 completed
        }
    }
    

}
