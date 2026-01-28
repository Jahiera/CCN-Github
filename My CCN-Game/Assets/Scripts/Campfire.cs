using UnityEngine;
using UnityEngine.InputSystem;

public class Campfire : MonoBehaviour
{
    private bool playerInRange = false;

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
            }
        }
    }
}