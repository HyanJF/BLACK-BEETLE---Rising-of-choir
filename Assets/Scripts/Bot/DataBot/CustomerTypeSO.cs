using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CustomerType",
    menuName = "Customers/Customer Type"
)]
public class CustomerTypeSO : ScriptableObject
{
    [Header("Goals")]
    public int requiredDrinks = 2;
    public int requiredSocialActivities = 2;

    [Header("Needs - Maximum Values")]
    public float maxThirst = 100f;
    public float maxComfort = 100f;
    public float maxBladder = 100f;

    [Header("Needs - Initial Values")]
    public float thirst = 0f;
    public float comfort = 0f;
    public float bladder = 0f;

    [Header("Needs - Increase Per Second")]
    public float thirstRate = 2f;
    public float comfortRate = 1f;
    public float bladderRate = 0.5f;

    [Header("Decision Thresholds")]
    [Range(0f, 1f)]
    public float thirstThreshold = 0.7f;

    [Range(0f, 1f)]
    public float comfortThreshold = 0.4f;

    [Range(0f, 1f)]
    public float bladderThreshold = 0.8f;

    [Header("Patience")]
    public float maxPatience = 100f;
    public float patienceRecoveryRate = 2f;
    public float patienceLossRate = 10f;

    [Header("Mood")]
    public float maxHappiness = 100f;

    [Range(0f, 1f)]
    public float angryThreshold = 0.5f;

    [Range(0f, 1f)]
    public float joyThreshold = 0.5f;

    public float moodMultiplier = 1f;
    public float tipMultiplier = 1f;
    public int maxTolerance = 1;

    [Header("Drinks Range")]
    public int minDrinksPerOrder = 1;
    public int maxDrinksPerOrder = 1;

    [Header("Type")]
    public string displayName;

    [Header("Drink Preferences")]
    public List<WeightedDrink> drinkPreferences =
        new();

    [Header("Profile")]
    public CustomerProfileSO profile;

    public DrinkType GetRandomDrink()
    {
        if (drinkPreferences.Count == 0)
            return DrinkType.None;

        int totalWeight = 0;

        foreach (WeightedDrink preference in drinkPreferences)
        {
            totalWeight += preference.weight;
        }

        int random =
            Random.Range(0, totalWeight);

        foreach (WeightedDrink preference in drinkPreferences)
        {
            random -= preference.weight;

            if (random < 0)
                return preference.drink;
        }

        return drinkPreferences[0].drink;
    }
}