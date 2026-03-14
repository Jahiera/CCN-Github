using UnityEngine;
using UnityEngine.InputSystem;

public class Campfire : MonoBehaviour
{
    private bool playerInRange = false;
    private Animator animator;
    
    [SerializeField] private AudioSource fireAudio;
    private bool fireStarted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // ======================
    // PLAYER ENTERS CAMPFIRE AREA
    // ======================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // ======================
    // PLAYER LEAVES CAMPFIRE AREA
    // ======================
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // ======================
    // PRESS E TO PLACE LOG
    // ======================
    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            GameManager gm =
                GameObject.FindFirstObjectByType<GameManager>();

            if (gm != null && gm.hasLog)
            {
                gm.PlaceLog();
                
                UpdateCampfireAnimation(gm.CollectedCount);
            }
        }
    }
    
    void UpdateCampfireAnimation(int logsPlaced)
    {
        if (animator == null) return;
        animator.SetInteger("logCount", logsPlaced);
        
        // start fire sound after first log
        if (!fireStarted && logsPlaced >0)
        {
            fireStarted = true;
            fireAudio.Play(); 
        }
        
    }

}