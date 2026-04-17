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
    
    //Hiding (do not remove bc other systems rely on it)
    public bool isHiding {get; private set;}
    
    //Gravity Setup 
    [SerializeField] private float normalGravity = 2f;
    [SerializeField] private float fallGravity = 3.5f;
    [SerializeField] private float jumpBoost = 1.15f; // <- this is a multiplier, not a new force FYI 
    
    //LVL3 Climbing Setup
    [SerializeField] private float climbSpeed = 4f;

    private bool isOnLadder = false;
    private bool isClimbing = false;

    private float verticalInput;
   

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>(); // check which scene is playing to disable level 2 jump
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            canJump = false;
        }
        
       //Default Gravity 
       rb.gravityScale = normalGravity; 
       
       
        
    }
    
    void Update()
    {

       

        if (isHiding) return;
        
        
        if (Keyboard.current == null) return;

        // Horizontal Input
        horizontalInput = 0f;
        if (Keyboard.current.aKey.isPressed) horizontalInput = -1f; // go left 
        if (Keyboard.current.dKey.isPressed) horizontalInput = 1f; // go right 
        
        //vertical input

        verticalInput = 0f;

        if (Keyboard.current.wKey.isPressed) verticalInput = 1f;
        if (Keyboard.current.sKey.isPressed) verticalInput = -1f;
        

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
        
        //LVL3 Climbing Logic

        if (isOnLadder && Mathf.Abs(verticalInput) > 0)
        {
            isClimbing = true;
        }
        else if (verticalInput == 0)
        {
            isClimbing = false;
        }

    }

    void FixedUpdate()
    {
        // Ground check
             isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
             
        //LVL3 Climbing
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(0f, verticalInput * climbSpeed);
            return;
        }
        
        

        // Horizontal movement
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Jump
        if (jumpPressed && isGrounded && canJump) // can jump in order to check for level 2 scene
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * jumpBoost);
            jumpPressed = false;
        }
        
        //Adjust gravity dynamically 
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = fallGravity;   // Falling faster
        }
        else
        {
            rb.gravityScale = normalGravity; // Rising / grounded
        }
        
    }
    
    public void SetHiding(bool value)
    {
        isHiding = value;
    }
    
    //LVL3 Climbing voids
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isOnLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            isOnLadder = false;
            isClimbing = false;
            
            rb.gravityScale = normalGravity;
        }
    }
}