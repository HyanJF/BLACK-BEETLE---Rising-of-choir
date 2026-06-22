using System;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public event Action<BotController>
        OnBotFinished;

    public Blackboard Blackboard { get; private set; }

    public BotMovement Movement { get; private set; }

    public BotNavigation Navigation { get; private set; }

    public BotBrain Brain { get; private set; }

    public BotNeeds Needs { get; private set; }

    public VisitGoals Goals { get; private set; }

    public BotMood Mood { get; private set; }

    public BotVisualController VisualController
    {
        get;
        private set;
    }

    protected StateMachine stateMachine;

    public StateMachine StateMachine =>
        stateMachine;

    protected virtual void Awake()
    {
        Movement =
            GetComponent<BotMovement>();

        Navigation =
            GetComponent<BotNavigation>();

        Brain =
            GetComponent<BotBrain>();

        Needs =
            GetComponent<BotNeeds>();

        VisualController =
            GetComponent<BotVisualController>();

        Mood =
            GetComponent<BotMood>();

        ResetRuntimeData();
    }

    protected virtual void Update()
    {
        stateMachine?.Update();
    }

    public void ActivateBot(
        CustomerTypeSO type)
    {
        ResetRuntimeData();

        Initialize(type);

        Blackboard.CurrentRoute =
            (RouteType)
            UnityEngine.Random.Range(
                0,
                5
            );

        gameObject.SetActive(true);

        stateMachine.ChangeState(
            new ThinkState(this)
        );
    }

    public void DeactivateBot()
    {
        stateMachine =
            new StateMachine();

        gameObject.SetActive(false);
    }

    public void FinishVisit()
    {
        OnBotFinished?.Invoke(this);
    }

    private void ResetRuntimeData()
    {
        Goals =
            new VisitGoals();

        Blackboard =
            new Blackboard();

        stateMachine =
            new StateMachine();
    }

    public void Initialize(
        CustomerTypeSO type)
    {
        Goals.RequiredDrinks =
            type.requiredDrinks;

        Goals.RequiredSocialActivities =
            type.requiredSocialActivities;

        Needs.thirst =
            type.thirst;

        Needs.comfort =
            type.comfort;

        Needs.bladder =
            type.bladder;

        Needs.maxPatience =
            type.maxPatience;

        Needs.currentPatience =
            type.maxPatience;

        Needs.patienceRecoveryRate =
            type.patienceRecoveryRate;

        Needs.patienceLossRate =
            type.patienceLossRate;

        Mood.maxHappiness =
            type.maxHappiness;

        Mood.happiness =
            type.maxHappiness;

        Mood.moodMultiplier =
            type.moodMultiplier;
    }
}