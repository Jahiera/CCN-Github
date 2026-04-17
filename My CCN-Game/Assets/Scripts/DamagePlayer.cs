using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 1;
    public bool instantDeath = false; 
    
    //changes
    public bool constantDamage = false;
    public float damageInterval = 1f;
    private float timer;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!constantDamage) return;
        if (!collision.CompareTag("Player")) return;

        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health == null) return;

        timer += Time.deltaTime;

        if (timer >= damageInterval)
        {
            if (instantDeath)
            {
                health.DieInstant();
            }
            else
            {
                health.TakeDamage(damage);
            }
            timer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!constantDamage) return;
        if (collision.CompareTag("Player"))
        {
            timer = 0f;
        }
    }
}

