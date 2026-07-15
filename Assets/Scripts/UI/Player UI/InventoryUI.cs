using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory;

    [SerializeField]
    private InventorySlotUI[] slots;

    [SerializeField]
    private Image previousInputImage;

    [SerializeField]
    private Image nextInputImage;

    private void Start()
    {
        Refresh();

        RefreshInputIcons();
    }

    private void OnEnable()
    {
        inventory.OnInventoryChanged += Refresh;

        InputDeviceManager.Instance.OnDeviceChanged +=
            RefreshInputIcons;
    }

    private void OnDisable()
    {
        inventory.OnInventoryChanged -= Refresh;

        if (InputDeviceManager.Instance != null)
        {
            InputDeviceManager.Instance.OnDeviceChanged -=
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
                    GameDataBase.Instance.drinkData.GetSprite(inventory.Drinks[index]));

                slots[index].SetSelected(index == inventory.SelectedIndex);
            }
            else
            {
                slots[index].SetEmpty();

                slots[index].SetSelected(index == inventory.SelectedIndex);
            }
        }

        for (; index < slots.Length; index++)
        {
            slots[index].SetLocked();
        }
    }

    private void RefreshInputIcons()
    {
        previousInputImage.sprite =
            InputDeviceManager.Instance.GetSprite(
                InputActionType.PreviousDrink);

        nextInputImage.sprite =
            InputDeviceManager.Instance.GetSprite(
                InputActionType.NextDrink);
    }
}