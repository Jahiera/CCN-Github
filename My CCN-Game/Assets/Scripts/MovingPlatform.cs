using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 lastPosition;

    private Rigidbody2D platformRb;
    private Rigidbody2D playerRb;
    private bool playerOnTop;

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;

        platformRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        Vector3 newPosition = startPosition + new Vector3(xOffset, 0f, 0f);

        Vector3 platformDelta = newPosition - lastPosition;

        if (platformRb != null)
            platformRb.MovePosition(newPosition);
        else
            transform.position = newPosition;

        if (playerOnTop && playerRb != null)
        {
            playerRb.position += new Vector2(platformDelta.x, platformDelta.y);
        }

        lastPosition = newPosition;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // Only carry player if they are ABOVE the platform
        if (collision.transform.position.y > transform.position.y)
        {
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerOnTop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerOnTop = false;
        playerRb = null;
    }
}