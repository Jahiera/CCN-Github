using System.Collections;
using UnityEngine;

public class TitleScreenIntro : MonoBehaviour
{
    [Header("Title Object")]
    public GameObject titleText;

    [Header("Blink Settings")]
    public float titleDelay = 1f;
    public float blinkSpeed = 0.08f;
    public int blinkCount = 8;

    [Header("Hover Settings")]
    public float hoverAmount = 5f;
    public float hoverSpeed = 2f;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip titleBlinkSound;

    private RectTransform rectTransform;
    private Vector2 startPos;

    private void Start()
    {
        if (titleText != null)
        {
            titleText.SetActive(false);

            rectTransform = titleText.GetComponent<RectTransform>();

            if (rectTransform != null)
                startPos = rectTransform.anchoredPosition;
        }

        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }

        StartCoroutine(ShowTitle());
    }

    private IEnumerator ShowTitle()
    {
        yield return new WaitForSeconds(titleDelay);

        if (sfxSource != null && titleBlinkSound != null)
            sfxSource.PlayOneShot(titleBlinkSound);

        for (int i = 0; i < blinkCount; i++)
        {
            titleText.SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);

            titleText.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
        }

        titleText.SetActive(true);

        StartCoroutine(HoverEffect());
    }

    private IEnumerator HoverEffect()
    {
        while (true)
        {
            if (rectTransform != null)
            {
                float yOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;

                rectTransform.anchoredPosition =
                    startPos + new Vector2(0, yOffset);
            }

            yield return null;
        }
    }
}