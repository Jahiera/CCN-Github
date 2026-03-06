using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //(array) creates a list input for unlimited patrol points
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        //So the enemy starts at 0
        targetPoint = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    { 
        //Movement code of the enemy
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        
        //Check the direction of target to flip the sprite
        if (patrolPoints[targetPoint].position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (patrolPoints[targetPoint].position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        
        //checks if the enemy reaches the point. If it does, increase it
        if (transform.position == patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }
        
    }
    

    //check our enemy reaches the point, make it move to the next point
    void increaseTargetInt()
    {
        targetPoint++;
        //make it stop checking for other points after it goes through the planned ones, makes it go back to 0 which will loop it
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
