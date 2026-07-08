using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory;

    [SerializeField]
    private InventorySlotUI[] slots;

    private void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        inventory.OnInventoryChanged += Refresh;
    }

    private void OnDisable()
    {
        inventory.OnInventoryChanged -= Refresh;
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
}