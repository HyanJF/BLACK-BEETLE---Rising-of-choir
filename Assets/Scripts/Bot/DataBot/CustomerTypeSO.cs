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

    [Header("Needs")]
    public float thirst;
    public float comfort;
    public float bladder;

    [Header("Patience")]
    public float maxPatience = 100f;
    public float patienceRecoveryRate = 2f;
    public float patienceLossRate = 10f;

    [Header("Mood")]
    public float maxHappiness = 100f;
    public float moodMultiplier = 1f;
}