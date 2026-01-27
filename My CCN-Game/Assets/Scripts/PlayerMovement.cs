using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    //Ground Detection
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool jumpPressed; // KG

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // (smooths jitter) 
    }


    void Update()
    {
        if (Keyboard.current == null) return;

        // Horizontal Input
        horizontalInput = 0f;
        if (Keyboard.current.aKey.isPressed) horizontalInput = -1f; // go left 
        if (Keyboard.current.dKey.isPressed) horizontalInput = 1f; // go right 
        //horizontalInput = moveLeft + moveRight;

        // Jump Input
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpPressed = true;
            // rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Horizontal movement
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Jump
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }
    
}