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

            if (instantDeath)
            {
                health.DieInstant();
            }
            else
            {
                health.TakeDamage(damage);
            }
        }
    }
}

