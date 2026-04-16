using UnityEngine;
using System.Collections;

public class PlayerThoughtBubble : MonoBehaviour
{
    [Header("Thought Bubble")]
    public GameObject thoughtBubble;
    public float showDuration = 2f;

    private Coroutine bubbleCoroutine;

    private Vector3 originalScale;
    public SpriteRenderer bubbleSprite;

    private Transform player;

    private void Start()
    {
        if (thoughtBubble != null)
            thoughtBubble.SetActive(false);

        originalScale = thoughtBubble.transform.localScale;

        player = transform; // bubble is child of player

        if (bubbleSprite != null)
            bubbleSprite.flipX = false;
    }

    private void LateUpdate()
    {
        if (thoughtBubble == null || player == null) return;

        // cancel out player flip so bubble stays readable
        Vector3 scale = thoughtBubble.transform.localScale;

        scale.x = Mathf.Sign(player.localScale.x) < 0 ? -originalScale.x : originalScale.x;
        scale.y = originalScale.y;
        scale.z = originalScale.z;

        thoughtBubble.transform.localScale = scale;
    }

    public void ShowBubble()
    {
        if (thoughtBubble == null) return;

        if (bubbleCoroutine != null)
            StopCoroutine(bubbleCoroutine);

        bubbleCoroutine = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        thoughtBubble.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        thoughtBubble.SetActive(false);
    }
}