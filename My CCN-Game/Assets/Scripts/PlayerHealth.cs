using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    
    public Image healthImage;        // UI image for battery
    public Sprite[] healthSprites;  // 5 battery sprites (empty → full)
    public int maxHealth = 5;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Call this when the player takes damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

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

}

