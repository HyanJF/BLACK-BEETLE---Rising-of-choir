using System;
using UnityEngine;

public class BotMood : MonoBehaviour
{
    public float maxHappiness;
    public float Happiness { get; private set; }

    public int maxTolerance;
    public int Tolerance { get; private set; }

    public float moodMultiplier = 1f;
    public float tipMultiplier = 1f;

    public float AngryLimit { get; private set; }
    public float JoyLimit { get; private set; }


    public event Action OnHappinessChanged;
    public event Action OnToleranceChanged;
    public event Action OnToleranceDepleted;

    public void Initialize(CustomerTypeSO type)
    {
        maxHappiness = type.maxHappiness;
        SetHappiness(maxHappiness);

        maxTolerance = type.maxTolerance;
        SetTolerance(maxTolerance);

        moodMultiplier = type.moodMultiplier;
        tipMultiplier = type.tipMultiplier;

        AngryLimit =
            maxHappiness * type.angryThreshold;

        JoyLimit =
            maxHappiness * type.joyThreshold;
    }


    public void AddHappiness(float amount)
    {
        SetHappiness(
            Happiness + amount);
    }

    public void RemoveHappiness(float amount)
    {
        SetHappiness(
            Happiness - amount);
    }

    public void SetHappiness(float value)
    {
        value = Mathf.Clamp(
            value,
            0,
            maxHappiness);

        if (Mathf.Approximately(
            Happiness,
            value))
            return;

        Happiness = value;

        OnHappinessChanged?.Invoke();
    }

    public void AddTolerance(int amount = 1)
    {
        SetTolerance(
            Tolerance + amount);
    }

    public void RemoveTolerance(int amount = 1)
    {
        Debug.Log("RemoveTolerance");
        SetTolerance(
            Tolerance - amount);
    }

    public void SetTolerance(int value)
    {
        value = Mathf.Clamp(value, 0, maxTolerance);

        if (Tolerance == value)
            return;

        Tolerance = value;

        OnToleranceChanged?.Invoke();

        if (Tolerance == 0)
        {
            OnToleranceDepleted?.Invoke();
        }
    }
}