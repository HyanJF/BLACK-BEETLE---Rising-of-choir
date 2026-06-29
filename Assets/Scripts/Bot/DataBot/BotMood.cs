using UnityEditor;
using UnityEngine;

public class BotMood : MonoBehaviour
{
    public float maxHappiness = 100f;
    public float happiness = 100f;

    public float moodMultiplier = 2f;
    public float tipMultiplier = 1f;

    private void Awake()
    {
        happiness = maxHappiness;
    }
}
