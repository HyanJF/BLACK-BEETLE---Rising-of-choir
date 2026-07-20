using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory;

    [SerializeField]
    private InventorySlotUI[] slots;

    [SerializeField]
    private InputIcon[] inputIcons;

    private void Start()
    {
        Refresh();
        RefreshInputIcons();
    }

    private void OnEnable()
    {
        inventory.OnInventoryChanged += Refresh;

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInputDeviceChanged +=
                RefreshInputIcons;
        }
    }

    private void OnDisable()
    {
        inventory.OnInventoryChanged -= Refresh;

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInputDeviceChanged -=
                RefreshInputIcons;
        }
    }

    public void Refresh()
    {
        int index = 0;

        for (; index < inventory.UnlockedSlots; index++)
        {
            if (index < inventory.Count)
            {
                slots[index].SetDrink(
                    GameDataBase.Instance.drinkData.GetSprite(
                        inventory.Drinks[index]));

                slots[index].SetSelected(
                    index == inventory.SelectedIndex);
            }
            else
            {
                slots[index].SetEmpty();

                slots[index].SetSelected(
                    index == inventory.SelectedIndex);
            }
        }

        for (; index < slots.Length; index++)
        {
            slots[index].SetLocked();
        }
    }

    private void RefreshInputIcons()
    {
        foreach (InputIcon icon in inputIcons)
        {
            icon.image.sprite =
                InputManager.Instance.GetSprite(icon.action);
        }
    }
}