using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public event Action OnMoneyChanged;
    public event Action<float> OnMoneyAdded;
    public event Action<float> OnMoneySpent;

    public float Money
    {
        get;
        private set;
    }

    public void AddMoney(float amount)
    {
        Money += amount;

        OnMoneyChanged?.Invoke();
        OnMoneyAdded?.Invoke(amount);
    }

    public void SpendMoney(float amount)
    {
        Money -= amount;

        OnMoneyChanged?.Invoke();
        OnMoneySpent?.Invoke(amount);
    }

    public bool TrySpendMoney(float amount)
    {
        if (Money < amount)
            return false;

        SpendMoney(amount);

        return true;
    }

    public void ReceiveDrinkPayment(
    DrinkType drink,
    BotController bot)
    {
        float basePrice =
            GameDataBase.Instance
            .drinkData
            .GetPrice(drink);

        float happiness =
            bot.Mood.Happiness /
            bot.Mood.maxHappiness;

        float maxTipPercent =
            0.30f;

        float tip =
            basePrice *
            maxTipPercent *
            happiness *
            bot.Mood.moodMultiplier;

        AddMoney(basePrice);

        if (tip > 0.01f)
            AddMoney(tip);
    }
}