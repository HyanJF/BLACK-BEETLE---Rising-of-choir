using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Image background;

    [Header("Colors")]
    [SerializeField] private Color emptyColor = Color.gray;
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Color lockedColor = Color.black;

    private bool isLocked;

    public void SetLocked()
    {
        isLocked = true;

        icon.enabled = false;
        icon.sprite = null;

        background.enabled = enabled; 
        background.color = lockedColor;
    }

    public void SetEmpty()
    {
        isLocked = false;

        icon.enabled = false;
        icon.sprite = null;

        background.enabled = true;
        background.color = emptyColor;
    }

    public void SetDrink(Sprite sprite)
    {
        isLocked = false;

        icon.enabled = true;
        icon.sprite = sprite;

        background.enabled = true;
    }

    public void SetSelected(bool value)
    {
        if (isLocked)
            return;

        background.color = value ? selectedColor : emptyColor;

        if (icon.enabled)
            icon.color = value ? selectedColor : emptyColor;
    }
}