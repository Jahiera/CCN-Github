using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalTeleport : MonoBehaviour
{
    public Transform teleportDestination;
    public Image fadeImage;
    public float fadeDuration = 0.25f;
    
    [Header("Level Completion")]
    public bool isFinalPortal = false;
    
    [Header("Level 2 Animation Change")]
    public string animatorTriggerName;

    private bool isTeleporting;
    
    [Header("Level 2 Music Switch")]
    public AudioSource musicToStop;
    public AudioSource musicToStart;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(Teleport(other.transform));
        }
    }

    IEnumerator Teleport(Transform player)
    {
        isTeleporting = true;

        // Fade OUT
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Teleport player
        player.position = teleportDestination.position;
        
        //music change from school to park 
        if (musicToStop != null)
        {
            musicToStop.Stop();
        }

        if (musicToStart != null && !musicToStart.isPlaying)
        {
            musicToStart.loop = true;
            musicToStart.Play();
        }
        
        // animation change
        Animator playerAnimator = player.GetComponent<Animator>();

        if (playerAnimator != null && !string.IsNullOrEmpty(animatorTriggerName))
        {
            playerAnimator.SetTrigger(animatorTriggerName);
        }
        
        //check final portal to end level
        if (isFinalPortal)
        {
            PlayerPrefs.SetInt("Level2Completed", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MainMenu");
        }

        // Fade IN
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        isTeleporting = false;
    }
}