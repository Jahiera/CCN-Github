using UnityEngine;
using UnityEngine.SceneManagement;

public class RisingWater : MonoBehaviour
{
    public float riseSpeed = 2f;
    
    // start delay
    public float startDelay = 3f;

    private bool canRise = false;
    
    [Header("Optional Main Menu Trigger")]
    public bool returnToMenuTrigger = false;
    
    
    private void Start()
    {
        Invoke(nameof(StartRising), startDelay);
    }

    private void StartRising()
    {
        canRise = true;
    }

    void Update()
    {
        // can start rising after delay 
        if (canRise)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (returnToMenuTrigger && collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    
}
