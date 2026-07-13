using System;
using UnityEngine;

public class BarStockInventory : MonoBehaviour
{
    [SerializeField]
    private int maxStock = 10;

    public event Action OnStockChanged;

    public int CurrentStock
    {
        get;
        private set;
    }

    public int MaxStock => maxStock;

    public bool IsEmpty =>
        CurrentStock <= 0;

    public bool IsFull =>
        CurrentStock >= maxStock;

    private void Start()
    {
        Restock();
    }

    public bool Consume()
    {
        if (IsEmpty)
            return false;

        CurrentStock--;

        OnStockChanged?.Invoke();

        InteractableSounds.instance.TakedrinkPlaySound();

        return true;
    }

    public void Restock()
    {
        CurrentStock = maxStock;

        OnStockChanged?.Invoke();

        InteractableSounds.instance.RestoredPlaySound();
    }
}