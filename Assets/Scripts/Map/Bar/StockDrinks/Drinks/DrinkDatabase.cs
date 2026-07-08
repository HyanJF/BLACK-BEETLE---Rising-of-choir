using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "DrinkDatabase",
    menuName = "Drinks/Drink Database"
)]
public class DrinkDatabase : ScriptableObject
{
    [SerializeField]
    private List<DrinkData> drinks =
        new();

    public DrinkData GetDrink(DrinkType type)
    {
        foreach (DrinkData drink in drinks)
        {
            if (drink.type == type)
                return drink;
        }

        return null;
    }

    public float GetPrice(DrinkType type)
    {
        DrinkData drink = GetDrink(type);

        return drink != null
            ? drink.price
            : 0f;
    }

    public float GetRestockPrice(DrinkType type)
    {
        DrinkData drink = GetDrink(type);

         return drink != null
            ? drink.buyPrice
            : 0f;
    }

    public Sprite GetSprite(
        DrinkType type)
    {
        foreach (DrinkData drink in drinks)
        {
            if (drink.type == type)
                return drink.icon;
        }

        return null;
    }

    public float GetSatisfaction(DrinkType drink)
    {
        DrinkData data = GetDrink(drink);

        return data != null
            ? data.satisfaction
            : 0f;
    }
}