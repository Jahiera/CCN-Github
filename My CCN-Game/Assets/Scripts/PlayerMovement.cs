using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private bool canJump = true; // to disable jump in level 2
    
    //Hiding spot
    public bool isHiding {get; private set;}
    private bool canHide = false;
    private SpriteRenderer spriteRenderer;

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

            if (isHiding)
            {
                isHiding = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
        
        rb = GetComponent<Rigidbody2D>(); // check which scene is playing to disable level 2 jump
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            canJump = false;
        }
        
    }
    
    void Update()
    {

        //Hiding input
        if (Keyboard.current.eKey.wasPressedThisFrame && canHide)
        {
            isHiding = !isHiding; //Toggles between true and false

            if (isHiding)
            {
                rb.linearVelocity = Vector2.zero; //Stops movement
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
        }

        if (isHiding) return;
        
        
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
        if (jumpPressed && isGrounded && canJump) // can jump in order to check for level 2 scene
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }
    
}