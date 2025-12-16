using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   public float moveSpeed = 5f;
   public float jumpForce = 10f;
   
   //ground detection
   public Transform groundCheck;
   public float groundCheckRadius = 0.2f;
   public LayerMask groundLayer;
   
   private Rigidbody2D rb;
   private float horizontalInput;
   private bool isGrounded;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Keyboard.current == null) return;
        
        float moveLeft = Keyboard.current.aKey.isPressed ? -1f : 0f;
        float moveRight = Keyboard.current.dKey.isPressed ? 1f : 0f;
        horizontalInput = moveLeft + moveRight;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
