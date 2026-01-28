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
    private Animator animator;
    private float horizontalInput;
    private bool isGrounded;
    private bool jumpPressed; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // (smooths jitter)
    }


    void Update()
    {
        if (Keyboard.current == null) return;

        // Horizontal Input
        horizontalInput = 0f;
        if (Keyboard.current.aKey.isPressed) horizontalInput = -1f; // go left 
        if (Keyboard.current.dKey.isPressed) horizontalInput = 1f; // go right 
        

        // Jump Input
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpPressed = true;
        }
        
        // ======================
        // ANIMATION: IDLE / WALK
        // ======================
        bool isMoving = Mathf.Abs(horizontalInput) > 0.01f;
        animator.SetBool("IsMoving", horizontalInput != 0);
        animator.SetBool("IsGrounded", isGrounded);
        bool isFalling = !isGrounded && rb.linearVelocity.y < -0.1f;
        animator.SetBool("IsFalling", isFalling);
        
        // ======================
        // FLIP PLAYER SPRITE
        // ======================
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(horizontalInput),
                1,
                1
            );
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