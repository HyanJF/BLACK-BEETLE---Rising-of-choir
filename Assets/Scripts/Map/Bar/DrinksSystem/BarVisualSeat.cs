using UnityEngine;

public class BarVisualSeat : MonoBehaviour
{
    #region References

    [Header("Client Visual")]
    [SerializeField] private SpriteRenderer clientSprite;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.black;
    [SerializeField] private Color focusColor = Color.white;

    #endregion

    private bool focused;

    private void Start()
    {
        clientSprite.enabled = false;
    }

    public void SetFocused(bool value)
    {
        focused = value;

        if (clientSprite == null)
            return;

        clientSprite.color =
            focused ? focusColor : normalColor;
    }

    public void ShowClient()
    {
        clientSprite.enabled = true;
    }

    public void HideClient()
    {
        clientSprite.enabled = false;
    }
}