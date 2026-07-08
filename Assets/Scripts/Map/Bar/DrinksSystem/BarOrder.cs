using System;
using System.Collections.Generic;
using UnityEngine;

public class BarOrder : MonoBehaviour
{
    public Action OnOrderCompleted;
    public Action OnOrderUpdated;

    private readonly Queue<DrinkType> drinks = new();

    private int totalDrinks;

    public bool IsCompleted =>
        drinks.Count == 0;

    public DrinkType CurrentDrink =>
        drinks.Count > 0
        ? drinks.Peek()
        : DrinkType.None;

    public int RemainingDrinks =>
        drinks.Count;

    public int TotalDrinks =>
        totalDrinks;

    public int ServedDrinks =>
        totalDrinks - drinks.Count;

    public void CreateOrder(BotController bot)
    {
        drinks.Clear();

        totalDrinks = UnityEngine.Random.Range(
            bot.CustomerType.minDrinksPerOrder,
            bot.CustomerType.maxDrinksPerOrder + 1
            );

        for (int i = 0; i < totalDrinks; i++)
        {
            drinks.Enqueue(
                bot.CustomerType.GetRandomDrink());
        }

        OnOrderUpdated?.Invoke();
    }

    public ServeResult ServeDrink(DrinkType drink)
    {
        if (drink == DrinkType.None)
            return ServeResult.EmptyHands;

        if (CurrentDrink != drink)
            return ServeResult.WrongDrink;

        drinks.Dequeue();

        OnOrderUpdated?.Invoke();

        if (IsCompleted)
        {
            OnOrderCompleted?.Invoke();
            return ServeResult.OrderFinished;
        }

        return ServeResult.Success;
    }

    public void ClearOrder()
    {
        drinks.Clear();

        totalDrinks = 0;
        OnOrderUpdated?.Invoke();
    }

}