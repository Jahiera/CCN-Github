using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 1;
    public bool instantDeath = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();

            if (health != null)
            {
                if (instantDeath) // if in contact with water
                {
                    health.DieInstant(); //die immediately
                }
                health.TakeDamage(damage); // if NOT with water, take damage 
            }
        }
    }
}

