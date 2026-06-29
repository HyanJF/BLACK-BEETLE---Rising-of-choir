using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Capacity")]
    [SerializeField] private int maxCapacity = 7;
    [SerializeField] private int unlockedSlots = 2;

    private readonly List<DrinkType> drinks = new();

    public event Action OnInventoryChanged;

    public int MaxCapacity => maxCapacity;
    public int UnlockedSlots => unlockedSlots;

    public int Count => drinks.Count;

    public int SelectedIndex { get; private set; }

    public bool IsEmpty => drinks.Count == 0;

    public bool IsFull =>
        drinks.Count >= Mathf.Min(maxCapacity, unlockedSlots);

    public DrinkType CurrentDrink =>
        IsEmpty ? DrinkType.None : drinks[SelectedIndex];

    public IReadOnlyList<DrinkType> Drinks => drinks;

    public void UnlockSlot()
    {
        if (unlockedSlots >= maxCapacity)
            return;

        unlockedSlots++;
        OnInventoryChanged?.Invoke();
    }

    public bool AddDrink(DrinkType drink)
    {
        if (IsFull)
            return false;

        drinks.Add(drink);

        if (drinks.Count == 1)
            SelectedIndex = 0;

        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool ConsumeCurrentDrink()
    {
        if (IsEmpty)
            return false;

        drinks.RemoveAt(SelectedIndex);

        if (SelectedIndex >= drinks.Count)
            SelectedIndex = Mathf.Max(0, drinks.Count - 1);

        OnInventoryChanged?.Invoke();
        return true;
    }

    public void SelectNext()
    {
        if (drinks.Count <= 1)
            return;

        SelectedIndex++;
        if (SelectedIndex >= drinks.Count)
            SelectedIndex = 0;

        OnInventoryChanged?.Invoke();
    }

    public void SelectPrevious()
    {
        if (drinks.Count <= 1)
            return;

        SelectedIndex--;
        if (SelectedIndex < 0)
            SelectedIndex = drinks.Count - 1;

        OnInventoryChanged?.Invoke();
    }

    public bool RemoveRandomDrink()
    {
        if (IsEmpty)
            return false;

        int randomIndex =
            UnityEngine.Random.Range(0, drinks.Count);

        drinks.RemoveAt(randomIndex);

        if (SelectedIndex >= drinks.Count)
            SelectedIndex = Mathf.Max(0, drinks.Count - 1);

        OnInventoryChanged?.Invoke();

        return true;
    }

    public void Clear()
    {
        drinks.Clear();
        SelectedIndex = 0;
        OnInventoryChanged?.Invoke();
    }
}