using UnityEngine;

public class Collectable : MonoBehaviour
{

    // ======================
    // WHEN PLAYER TOUCHES LOG
    // ======================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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