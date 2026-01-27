using UnityEngine;


public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            GameManager gm = GameObject.FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.CollectItem();
            }
            
            // Destroy Log
            Destroy(gameObject);
        }
    }
}
