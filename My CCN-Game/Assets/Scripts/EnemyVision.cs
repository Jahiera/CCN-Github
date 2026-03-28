using System.Collections;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public bool isFacingFront { get; private set; }
    public float embarrassmentRadius = 4f;

    public SpriteRenderer enemySprite;
    public Sprite safeSprite;
    public Sprite warningSprite;
    public Sprite dangerSprite;

    void Start()
    {
        StartCoroutine(LookAroundRoutine());
    }

    IEnumerator LookAroundRoutine()
    {
        while (true)
        {
            // SAFE
            isFacingFront = false;
            enemySprite.sprite = safeSprite;
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            // WARNING
            enemySprite.sprite = warningSprite;
            yield return new WaitForSeconds(0.5f);

            // DANGER
            isFacingFront = true;
            enemySprite.sprite = dangerSprite;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}