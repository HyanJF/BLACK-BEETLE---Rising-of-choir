using UnityEngine;

public class BarStockVisualController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private GameObject input;

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

        input.SetActive(value);
    }
}