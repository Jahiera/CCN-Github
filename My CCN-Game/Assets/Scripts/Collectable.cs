using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioClip pickupSound;       // Drag sound here in Inspector
    private AudioSource audioSource;    // The AudioSource component to play it
    
    // To make the prefabs flightly float
    public float floatAmplitude = 0.25f; // how high it floats
    public float floatFrequency = 2f;    // how fast it floats

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // store original position
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false; // make sure it doesn't play automatically
        audioSource.loop = false; // one shot 
        
    }
    
    void Update()
    {
        // Move object up and down like retro collectibles
        Vector3 pos = startPos;
        pos.y += Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = pos;
    }

    // ======================
    // WHEN PLAYER TOUCHES LOG
    // ======================
    // ======================
// WHEN PLAYER TOUCHES LOG
// ======================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // pickup log
            GameManager gm =
                GameObject.FindFirstObjectByType<GameManager>();

            if (gm == null) return;

            // Play the pick-up sound
            if (pickupSound != null)
            {
                // This plays the sound instantly, independent of the log's GameObject
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            if (gm.hasLog)
            {
                // SHOW THOUGHT BUBBLE
                PlayerThoughtBubble bubble =
                    collision.GetComponent<PlayerThoughtBubble>();

                if (bubble != null)
                {
                    bubble.ShowBubble();
                }

                return; // DO NOT PICK UP LOG
            }

            // Tell GameManager to PICK UP this log
            gm.CollectItem(gameObject);
        }
    }
}