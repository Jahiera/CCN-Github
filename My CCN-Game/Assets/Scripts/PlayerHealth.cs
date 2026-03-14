using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;  


public class PlayerHealth : MonoBehaviour
{
    
    public Image healthImage;        // UI image for battery
    public Sprite[] healthSprites;  // 5 battery sprites (empty → full)
    public int maxHealth = 5;
    public int currentHealth;
    
    //ouch sound setup
    public AudioClip ouchSound;     
    private AudioSource audioSource;  
    
    //player blinks when damage taken 
    public SpriteRenderer playerSprite;   // drag your player sprite here
    public float blinkDuration = 1f;      // total blinking time
    public float blinkInterval = 0.1f;    // time between on/off blinks

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        
    }

    // Call this when the player takes damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        
        // Play ouch sound
        if (audioSource != null && ouchSound != null)
        {
            audioSource.PlayOneShot(ouchSound);
        }
        
        //blinking 
        if (playerSprite != null)
        {
            StopAllCoroutines();          // stop any previous blinking
            StartCoroutine(BlinkSprite());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void DieInstant()
    {
        currentHealth = 0;
        UpdateHealthUI();
        Die();
    }

    void Die()
    {
        Debug.Log("Player died");
        Invoke(nameof(Restart), 0.5f); // 0.5 sec delay when dying 
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void UpdateHealthUI()
    {
        if (healthImage == null || healthSprites.Length == 0) return;

        // Clamp spriteIndex to valid range 0 -> healthSprites.Length-1
        int spriteIndex = Mathf.Clamp(currentHealth, 0, healthSprites.Length) - 1;

        // If health is 0, show the first (empty) sprite
        if (spriteIndex < 0) spriteIndex = 0;

        healthImage.sprite = healthSprites[spriteIndex];
    }
    
    //player blinks when damage is taken 
    private IEnumerator BlinkSprite()
    {
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            if (playerSprite != null)
                playerSprite.enabled = !playerSprite.enabled; // toggle sprite visibility

            elapsed += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        if (playerSprite != null)
            playerSprite.enabled = true; // make sure sprite is visible at the end
    }

}

