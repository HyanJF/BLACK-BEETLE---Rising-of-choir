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

    public BotVisualController VisualController { get; private set; }

    public BotThoughtVisual Thought { get; private set; }
    public CustomerTypeSO CustomerType {  get; private set; }
    public CustomerProfileSO Profile { get; private set; }
    public string DebugInfo =>
    $"{name}\n" +
    $"{CustomerType?.displayName ?? "None"}\n" +
    $"{StateMachine.CurrentState.GetType().Name}\n" +
    $"Tolerancia Maxima {Mood.MaxTolerance}\n" +
    $"Tolerancia Actual {Mood.Tolerance}";

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

        Thought =
            GetComponentInChildren<BotThoughtVisual>();

        ResetRuntimeData();
    }

    protected virtual void Update()
    {
        stateMachine?.Update();
    }

    public void ActivateBot(
    CustomerTypeSO type,
    CustomerProfileSO profileSO)
    {
        ResetRuntimeData();

        Initialize(
            type,
            profileSO);

        VisualController.ShowBot();
        Thought.DisableThought();

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

        CustomerType = null;
        Profile = null;
    }

    public void Initialize(
        CustomerTypeSO type,
        CustomerProfileSO profile)
    {
        CustomerType = type;

        Profile = profile;

        Needs.Initialize(type);

        Mood.Initialize(type);

        Goals.Initialize(type);
    }
}