using UnityEngine;

public class HidingSpotVisual : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite hiddenSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    public void SetHiddenSprite()
    {
        if (hiddenSprite != null)
        {
            spriteRenderer.sprite = hiddenSprite;
        }
    }

    public void SetNormalSprite()
    {
        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}