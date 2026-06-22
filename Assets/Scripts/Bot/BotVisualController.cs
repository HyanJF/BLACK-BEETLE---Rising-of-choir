using UnityEngine;

public class BotVisualController : MonoBehaviour
{
    private SpriteRenderer botSprite;
    private Collider2D botCollider;

    private void Start()
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

}
