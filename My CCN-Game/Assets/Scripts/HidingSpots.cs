using UnityEngine;
using UnityEngine.InputSystem;

public class HidingSpots : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private bool canHide = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame && canHide)
        {
            ToggleHide();
        }
    }

    private void ToggleHide()
    {
        playerMovement.SetHiding(!playerMovement.isHiding);

        if (playerMovement.isHiding)
        {
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            spriteRenderer.enabled = false;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HidingSpot"))
        {
            canHide = false;

            // IMPORTANT: don't force unhide here anymore
            // this fixes the snap-out bug
        }
    }
}