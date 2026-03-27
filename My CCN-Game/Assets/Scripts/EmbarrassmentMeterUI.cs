using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EmbarrassmentMeterUI : MonoBehaviour
{
    public float currentEmbarrassment = 0f;
    public float maxEmbarrassment = 100f;
    public float embarrassmentRate = 80f;   // per second
    public float cooldownRate = 15f;        // per second

    public Slider slider;

    // Track which enemies are currently seeing the player
    private HashSet<EnemyVision> enemiesSeeingPlayer = new HashSet<EnemyVision>(); //new

    void Start()
    {
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = maxEmbarrassment;
            slider.value = currentEmbarrassment;
        }
    }

    void Update()
    {
        // Use the HashSet count directly – no need for a separate int
        if (enemiesSeeingPlayer.Count > 0) //new
        {
            currentEmbarrassment += embarrassmentRate * Time.deltaTime;
            if (currentEmbarrassment > maxEmbarrassment)
                currentEmbarrassment = maxEmbarrassment;
        }
        else if (currentEmbarrassment > 0)
        {
            currentEmbarrassment -= cooldownRate * Time.deltaTime;
            if (currentEmbarrassment < 0)
                currentEmbarrassment = 0;
        }

        // Update slider
        if (slider != null)
            slider.value = currentEmbarrassment;

        // Restart scene if max embarrassment reached
        if (currentEmbarrassment >= maxEmbarrassment)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    // Called by enemies
    public void ReportSeeingPlayer(EnemyVision enemy, bool isSeeing) //new
    {
        if (isSeeing)
            enemiesSeeingPlayer.Add(enemy); //new
        else
            enemiesSeeingPlayer.Remove(enemy); //new
    }
}