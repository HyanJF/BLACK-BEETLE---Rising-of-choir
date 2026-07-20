using UnityEngine;

public class TrashVisualController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Color normalColor = Color.white;

    [SerializeField]
    private Color focusedColor = Color.green;

    private void Awake()
    {
        SetFocused(false);
    }

    public void SetFocused(bool focused)
    {
        sprite.color =
            focused
            ? focusedColor
            : normalColor;
    }
}
