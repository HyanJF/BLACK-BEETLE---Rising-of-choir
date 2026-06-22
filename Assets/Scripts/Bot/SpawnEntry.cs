using UnityEngine;

[System.Serializable]
public class SpawnEntry
{
    public CustomerTypeSO customerType;

    [Range(1, 100)]
    public int weight = 10;
}