using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform playerTransform;
    public PlayerMovement playerScript;
    public SpriteRenderer enemySprite;

    private EmbarrassmentMeterUI embarrassmentManager;

    // Vision settings
    public bool isFacingCamera = false; 
    public float sightDistance = 5f;
    public LayerMask obstacleLayer;

    // Changeable sprites
    public Sprite safeSprite;
    public Sprite warningSprite;
    public Sprite dangerSprite;

    void Start()
    {
        //wait one frame before grabbing embarrassmentManager to avoid null at start
        StartCoroutine(DelayedInit()); //fixed name //new
        StartCoroutine(LookAroundRoutine());
    }

    IEnumerator DelayedInit() //new
    {
        yield return null;
        embarrassmentManager = FindFirstObjectByType<EmbarrassmentMeterUI>();
    }

    IEnumerator LookAroundRoutine()
    {
        while (true)
        {
            // state 1: safe (looking away) 
            isFacingCamera = false;
            if (enemySprite != null && safeSprite != null) enemySprite.sprite = safeSprite;
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            // state 2: warning (about to turn)
            if (enemySprite != null && warningSprite != null) enemySprite.sprite = warningSprite;
            yield return new WaitForSeconds(0.5f);

            //state 3: danger (looking at camera)
            isFacingCamera = true;
            if (enemySprite != null && dangerSprite != null) enemySprite.sprite = dangerSprite;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    void Update()
    {
        if (playerTransform == null || playerScript == null || embarrassmentManager == null) return;

        bool seesPlayer = false;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= sightDistance)
        {
            Vector2 enemyEyes = new Vector2(transform.position.x, transform.position.y + 1f);
            Vector2 playerChest = new Vector2(playerTransform.position.x, playerTransform.position.y + 1f);
            Vector2 direction = (playerChest - enemyEyes).normalized;

            RaycastHit2D hit = Physics2D.Raycast(enemyEyes, direction, sightDistance, obstacleLayer);

            if (hit.collider == null && !playerScript.isHiding)
                seesPlayer = true;
        }

        //always report to embarrassment manager
        embarrassmentManager.ReportSeeingPlayer(this, seesPlayer); //new
    }
}