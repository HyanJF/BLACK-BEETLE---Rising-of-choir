using System.Collections.Generic;
using UnityEngine;

public class BarOrder : MonoBehaviour
{
    public System.Action OnOrderCompleted;

    [SerializeField] private BarVisualController visuals;

    private readonly Queue<DrinkType> drinks = new();

    public bool IsCompleted => drinks.Count == 0;

    public DrinkType CurrentDrink =>
        drinks.Count > 0 ? drinks.Peek() : 0;

    public void CreateOrder(BotController bot, int amount)
    {
        drinks.Clear();

        for (int i = 0; i < amount; i++)
        {
            drinks.Enqueue(
                bot.CustomerType.GetRandomDrink());
        }

        Debug.Log($"{bot.name} pidió: {string.Join(", ", drinks)}");

        UpdateVisual();
    }

    public bool ServeDrink(DrinkType drink)
    {
        if (IsCompleted)
            return false;

        if (CurrentDrink != drink)
            return false;

        CompleteCurrentDrink();
        return true;
    }

    public void CompleteCurrentDrink()
    {
        if (IsCompleted)
        {
            return;
        }

        drinks.Dequeue();

        visuals.PlayServeFeedback();

        UpdateVisual();

        if (IsCompleted)
        {
            Debug.Log("Pedido completado");
            OnOrderCompleted?.Invoke();
        }
    }

    public void ClearOrder()
    {
        drinks.Clear();
        visuals.HideDrink();
    }

    private void UpdateVisual()
    {
        if (IsCompleted)
        {
            visuals.HideDrink();
            return;
        }

        visuals.ShowDrink(CurrentDrink);
    }
}