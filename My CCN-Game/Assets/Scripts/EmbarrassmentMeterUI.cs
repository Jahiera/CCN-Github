using UnityEngine;
using UnityEngine.UI;

public class EmbarrassmentMeterUI : MonoBehaviour
{
    public Slider slider;
    public EnemyVision enemyVision;

    void Start()
    {
        if (slider != null && enemyVision != null)
        {
            slider.minValue = 0f;
            slider.maxValue = enemyVision.maxEmbarrassment;
            slider.value = enemyVision.currentEmbarrassment;
        }
    }

    void Update()
    {
        if (slider != null && enemyVision != null)
        {
            slider.value = enemyVision.currentEmbarrassment;
        }
    }
}