using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Call this when the player takes damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        Invoke(nameof(Restart), 1f); // 1 sec delay when dying 
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

