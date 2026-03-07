using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortalTeleport : MonoBehaviour
{
    public Transform teleportDestination;
    public Image fadeImage;
    public float fadeDuration = 0.25f;

    private bool isTeleporting;

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