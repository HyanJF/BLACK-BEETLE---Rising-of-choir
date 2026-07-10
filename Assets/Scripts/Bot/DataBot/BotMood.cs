using System;
using UnityEngine;

public class BotMood : MonoBehaviour
{
    public float Happiness { get; private set; }

    public int Tolerance { get; private set; }

    public float MaxHappiness { get; private set; }
    public int MaxTolerance { get; private set; }

    public float MoodMultiplier { get; private set; }
    public float TipMultiplier { get; private set; }

    public float AngryLimit { get; private set; }
    public float JoyLimit { get; private set; }

    public bool IsAngry =>
    Happiness <= AngryLimit;

    public bool IsHappy =>
        Happiness >= JoyLimit;

    public bool IsToleranceDepleted =>
        Tolerance <= 0;


    public event Action OnHappinessChanged;
    public event Action OnToleranceChanged;
    public event Action OnToleranceDepleted;

    public void Initialize(CustomerTypeSO type)
    {
        MaxHappiness = type.maxHappiness;
        SetHappiness(MaxHappiness);

        MaxTolerance = type.maxTolerance;
        SetTolerance(MaxTolerance);

        MoodMultiplier = type.moodMultiplier;
        TipMultiplier = type.tipMultiplier;

        CalculateMoodLimits(type);
    }

    private void CalculateMoodLimits(CustomerTypeSO type)
    {
        AngryLimit =
            MaxHappiness * type.angryThreshold;

        JoyLimit =
            MaxHappiness * type.joyThreshold;
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
            MaxHappiness);

        if (Mathf.Approximately(
            Happiness,
            value))
            return;

        Happiness = value;

        OnHappinessChanged?.Invoke();
    }

    public void AddTolerance(int amount)
    {
        SetTolerance(
            Tolerance + amount);
    }

    public void RemoveTolerance(int amount)
    {
        SetTolerance(
            Tolerance - amount);
    }

    public void SetTolerance(int value)
    {
        value = Mathf.Clamp(
            value, 
            0, 
            MaxTolerance);

        if (Tolerance == value)
            return;

        Tolerance = value;

        OnToleranceChanged?.Invoke();

        if (IsToleranceDepleted)
        {
            OnToleranceDepleted?.Invoke();
        }
    }
}