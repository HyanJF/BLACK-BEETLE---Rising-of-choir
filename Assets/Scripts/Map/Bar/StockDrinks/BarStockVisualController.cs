using UnityEngine;

public class BarStockVisualController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    private void Awake()
    {
        SetFocused(false);
    }

    public void SetFocused(bool value)
    {
        sprite.color =
            value ?
            Color.green :
            Color.white;
    }
}