using UnityEngine;

[System.Serializable]
public class DrinkData
{
    public DrinkType type;

    public Sprite icon;

    [Header("Economy")]
    public float price;

    [Header("Optional")]
    public string displayName;
}