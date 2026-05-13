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
    
    //ouch & drown sound setup
    public AudioClip ouchSound;     
    public AudioClip drowningSound;
    private bool useDrowningSound = false;
    private AudioSource audioSource;  
    
    //player blinks when damage taken 
    public SpriteRenderer playerSprite;   // drag your player sprite here
    public float blinkDuration = 1f;      // total blinking time
    public float blinkInterval = 0.1f;    // time between on/off blinks
    
    [Header("Level 3 Drowning Sprite")]
    public Sprite drowningSprite;

    private Sprite normalSprite;
    private bool useDrowningSprite = false;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        
        if (playerSprite != null)
        {
            normalSprite = playerSprite.sprite;
        }

        if (SceneManager.GetActiveScene().name == "Level3")
        {
            useDrowningSprite = true;
            useDrowningSound = true;
        }
        
        UpdateHealthUI();
        
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
        
        if (audioSource != null)
        {
            //play drown sound
            if (useDrowningSound && drowningSound != null)
            {
                audioSource.PlayOneShot(drowningSound);
            }
            //play ouch sound
            else if (ouchSound != null)
            {
                audioSource.PlayOneShot(ouchSound);
            }
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
        
        if (useDrowningSprite && animator != null)
        {
            animator.enabled = false;
        }

        while (elapsed < blinkDuration)
        {
            if (playerSprite != null)
                playerSprite.enabled = !playerSprite.enabled; // toggle sprite visibility
            
            //show drowning sprite
            if (useDrowningSprite && playerSprite.enabled)
            {
                playerSprite.sprite = drowningSprite;
            }
            else if (playerSprite.enabled)
            {
                playerSprite.sprite = normalSprite;
            }

            elapsed += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        if (playerSprite != null)
        {
            playerSprite.enabled = true;
            playerSprite.sprite = normalSprite;
        }

        if (useDrowningSprite && animator != null)
        {
            animator.enabled = true;
        }
    }

}

