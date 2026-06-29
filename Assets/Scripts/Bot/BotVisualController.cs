using UnityEngine;

public class BotVisualController : MonoBehaviour
{
    private SpriteRenderer botSprite;
    private Collider2D botCollider;

    [SerializeField]
    private BotThoughtVisual thought;

    private void Awake()
    {
        botSprite = GetComponent<SpriteRenderer>();
        botCollider = GetComponent<Collider2D>();
    }

    public void HideBot()
    {
        botSprite.enabled = false;
        botCollider.enabled = false;
    }

    public void ShowBot()
    {
        botSprite.enabled = true;
        botCollider.enabled = true;
    }

    public void HideEverything()
    {
        HideBot();

        if (thought != null)
            thought.DisableThought();
    }
}