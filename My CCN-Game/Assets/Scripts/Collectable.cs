using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip pickupSound;       // Drag sound here in Inspector
    private AudioSource audioSource;    // The AudioSource component to play it

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false; // make sure it doesn't play automatically
        audioSource.loop = false; // one shot 
        
    }

    // ======================
    // WHEN PLAYER TOUCHES LOG
    // ======================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Play the pick-up sound
            if (pickupSound != null)
            {
                // This plays the sound instantly, independent of the log's GameObject
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            
            // pickup log
            GameManager gm =
                GameObject.FindFirstObjectByType<GameManager>();

            if (gm != null)
            {
                // Tell GameManager to PICK UP this log
                gm.CollectItem(gameObject);
            }
        }
    }
}