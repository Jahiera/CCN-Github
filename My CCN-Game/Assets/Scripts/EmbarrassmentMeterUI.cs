using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmbarrassmentMeterUI : MonoBehaviour
{
    public float currentEmbarrassment = 0f;
    public float maxEmbarrassment = 100f;
    public float embarrassmentRate = 80f;
    public float cooldownRate = 15f;

    public Slider slider;
    public Transform playerTransform;
    public PlayerMovement playerScript;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = maxEmbarrassment;
        slider.value = currentEmbarrassment;
    }

    void Update()
    {
        bool embarrassingSituation = false;

        EnemyVision[] enemies = FindObjectsByType<EnemyVision>(FindObjectsSortMode.None);

        foreach (EnemyVision enemy in enemies)
        {
            float distance = Vector2.Distance(
                playerTransform.position,
                enemy.transform.position
            );

            if (
                enemy.isFacingFront &&
                distance <= enemy.embarrassmentRadius &&
                !playerScript.isHiding
            )
            {
                embarrassingSituation = true;
                break;
            }
        }

        if (embarrassingSituation)
            currentEmbarrassment += embarrassmentRate * Time.deltaTime;
        else
            currentEmbarrassment -= cooldownRate * Time.deltaTime;

        currentEmbarrassment = Mathf.Clamp(currentEmbarrassment, 0f, maxEmbarrassment);
        slider.value = currentEmbarrassment;

        if (currentEmbarrassment >= maxEmbarrassment)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}