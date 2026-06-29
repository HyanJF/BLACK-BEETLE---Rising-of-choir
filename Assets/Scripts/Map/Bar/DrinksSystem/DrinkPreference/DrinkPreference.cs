using UnityEngine;

[System.Serializable]
public class WeightedDrink
{
    public DrinkType drink;
    [Range(0, 100)]
    public int weight = 25;
}