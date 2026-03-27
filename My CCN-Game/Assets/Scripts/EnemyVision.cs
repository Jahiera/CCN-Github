using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyVision : MonoBehaviour
{
    public Transform playerTransform;
    public PlayerMovement playerScript;
    public SpriteRenderer enemySprite;
    
    //Vision settings
    public bool isFacingCamera = false;
    public float sightDistance = 5f;
    public LayerMask obstacleLayer;
    
    //embarrassment meter
    public float currentEmbarrassment = 0f;
    public float maxEmbarrassment = 100f;
    public float embarrassmentRate = 30f;
    
    //changable sprites
    public Sprite safeSprite;
    public Sprite warningSprite;
    public Sprite dangerSprite;
    void Start()
    {
        //automated timer of enemy sprites
        StartCoroutine(LookAroundRoutine());
    }

    IEnumerator LookAroundRoutine()
    {
        while (true) //looping
        {
            //State 1: safe (looking away)
            isFacingCamera = false;
            if (enemySprite != null && safeSprite != null) enemySprite.sprite = safeSprite;
            
            // random look duration
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            
            //State 2: warning (about to turn)
            if (enemySprite != null && warningSprite != null) enemySprite.sprite = warningSprite;
            yield return new WaitForSeconds(0.5f);
            
            //State 3: danger (looking at camera)
            isFacingCamera = true;
            if (enemySprite != null && dangerSprite != null) enemySprite.sprite = dangerSprite;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
    
    void Update()
    {
        if (playerTransform == null || playerScript == null) return;
        
        //If enemy has back turned, player is safe
        if (!isFacingCamera)
        {
            CoolDown();
            return; //skips all other checks
        }
        
        //If they are facing camera, check how close player is
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= sightDistance)
        {
            //Shoots Raycast to make sure there is nothing between them (added 1 value because it ws detecting the ground)
            Vector2 enemyEyes = new Vector2(playerTransform.position.x, playerTransform.position.y +1f);
            Vector2 playerChest = new Vector2(playerTransform.position.x, playerTransform.position.y +1f);
            
            //calculate distance between new points
            Vector2 directionToPlayer = (playerChest - enemyEyes).normalized;
            
            //shoot raycast from eyes not the anchor point at the bottom
            RaycastHit2D hit = Physics2D.Raycast(enemyEyes, directionToPlayer, sightDistance, obstacleLayer);
            
            //If nothing is blocking view
            if (hit.collider == null)
            {
                if (!playerScript.isHiding)
                {
                    IncreaseEmbarrassment();
                }
                else
                {
                    CoolDown();
                }
            }
            else
            {
                CoolDown(); //hidable object saved player
            }
        }
        else
        {
            CoolDown(); //player is too far
        }
        
        
    }
    
    
    
    void IncreaseEmbarrassment()
    {
        currentEmbarrassment += embarrassmentRate * Time.deltaTime;
        if (currentEmbarrassment >= maxEmbarrassment) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CoolDown()
    {
        if (currentEmbarrassment > 0)
        {
            currentEmbarrassment -= (embarrassmentRate / 2) * Time.deltaTime;
        }
    }
    


}
