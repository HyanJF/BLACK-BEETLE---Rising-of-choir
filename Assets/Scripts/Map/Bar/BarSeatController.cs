using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BarSeatController : MonoBehaviour
{
    #region Events

    public Action OnCustomerFinished;

    #endregion

    #region References

    public BarSeat Seat
    {
        get;
        private set;
    }

    public BotController Customer
    {
        get;
        private set;
    }

    #endregion

    #region Data

    private string currentDialogue;

    #endregion

    #region Interaction

    public bool CanInteract
    {
        get;
        set;
    }

    public string InteractionText
    {
        get;
        private set;
    }

    private bool playerInside;

    #endregion

    #region State Machine

    public BarSeatStateMachine StateMachine
    {
        get;
        private set;
    }

    #endregion

    #region Initialization

    private void Awake()
    {
        Seat = GetComponent<BarSeat>();

    }

    #endregion

    #region Unity

    private void Update()
    {
        StateMachine?.Update();
    }

    #endregion

    #region Customer

    public void ReceiveCustomer(
        BotController customer)
    {
        Customer = customer;

        Seat.VisualsSeat.ShowClient();

        Seat.Order.CreateOrder(customer);

        Seat.Order.OnOrderUpdated += RefreshCustomerUI;

        Seat.Order.OnOrderUpdated += RefreshThought;

        Customer.Mood.OnHappinessChanged += RefreshCustomerUI;

        RefreshCustomerUI();

        StateMachine =
            new BarSeatStateMachine();

        ChangeState(
            new GreetingBarSeatState(this));
    }

    public void FinishSession()
    {
        GameDataBase.Instance.actionUI.Hide();
        GameDataBase.Instance.dialogueUI.Hide();
        GameDataBase.Instance.customerUI.Hide();

        Seat.Order.OnOrderUpdated -= RefreshCustomerUI;

        Seat.Order.OnOrderUpdated -= RefreshThought;

        Customer.Mood.OnHappinessChanged -= RefreshCustomerUI;

        Seat.VisualsSeat.HideClient();

        Seat.Thought.Hide();

        Seat.Order.ClearOrder();

        Seat.Free();

        StateMachine.ChangeState(null);

        Customer = null;

        InteractionText = string.Empty;
        CanInteract = false;

        OnCustomerFinished?.Invoke();
    }

    #endregion

    #region Player Interaction

    public void SetInteraction(string text)
    {
        InteractionText = text;

        RefreshInteractionUI();
    }

    public void PlayerEnteredRange()
    {
        playerInside = true;

        RefreshInteractionUI();

        if (Customer != null)
        {
            RefreshDialogueUI();
            RefreshCustomerUI();
            RefreshThought();
        }
    }

    public void PlayerExitedRange()
    {
        playerInside = false;

        GameDataBase.Instance.actionUI.Hide();
        GameDataBase.Instance.dialogueUI.Hide();
        GameDataBase.Instance.customerUI.Hide();

        Seat.Thought.Hide();
    }

    public void OnPlayerInteract(
        PlayerManager player)
    {
        if (!CanInteract)
            return;

        StateMachine.CurrentState?
            .OnPlayerInteract(player);
    }

    private void RefreshInteractionUI()
    {
        if (!playerInside)
            return;

        if (Customer == null)
        {
            GameDataBase.Instance.actionUI.Hide();
            return;
        }

        GameDataBase.Instance.actionUI.Show(
            InteractionText);

        GameDataBase.Instance.actionUI.SetInteractable(
            CanInteract);
    }

    public void EnableInteraction()
    {
        CanInteract = true;
        RefreshInteractionUI();
    }

    public void DisableInteraction()
    {
        CanInteract = false;
        RefreshInteractionUI();
    }

    #endregion

    #region Dialogue

    public void ShowDialogue()
    {
        RefreshDialogueUI();
    }

    private void RefreshDialogueUI()
    {
        if (!playerInside)
            return;

        if (Customer == null)
            return;

        if (StateMachine?.CurrentState == null)
            return;

        if (StateMachine.CurrentState.Dialogue == DialogueType.None)
            return;

        GameDataBase.Instance.dialogueUI.Show(
            Customer,
            currentDialogue);
    }

    public float GetDialogueDuration(DialogueType type)
    {
        float dialogueTime = GameDataBase.Instance.dialogueTiming.GetDuration(type);

        return dialogueTime;
    }

    private void UpdateCurrentDialogue()
    {
        DialogueType type =
            StateMachine.CurrentState.Dialogue;

        if (type == DialogueType.None)
        {
            currentDialogue = string.Empty;
            return;
        }

        if (Customer.Profile.useGenericDialogue)
        {
            currentDialogue =
                GameDataBase.Instance.dialogueData
                    .GetRandomDialogue(type);
        }
        else
        {
            currentDialogue =
                Customer.Profile.GetRandomDialogue(type);
        }
    }
    #endregion

    #region Customer Info
    private void RefreshCustomerUI()
    {
        if (!playerInside)
            return;

        if (Customer == null)
            return;

        GameDataBase.Instance.customerUI.Show();

        GameDataBase.Instance.customerUI.SetOrder(
            Seat.Order.ServedDrinks,
            Seat.Order.TotalDrinks);

        GameDataBase.Instance.customerUI.SetHappiness(
            Customer.Mood.Happiness,
            Customer.Mood.MaxHappiness);
    }
    #endregion

    #region Thought
    public void ShowThought()
    {
        RefreshThought();
    }

    private void RefreshThought()
    {
        if (!playerInside)
            return;

        if (Customer == null)
            return;

        if (StateMachine?.CurrentState == null)
            return;

        ThoughtType thought =
            StateMachine.CurrentState.Thought;

        if (thought == ThoughtType.None)
        {
            Seat.Thought.Hide();
            return;
        }

        Seat.Thought.Show(
            thought,
            Seat.Order.CurrentDrink);
    }

    #endregion

    #region Drinks
    public void ServeDrink(PlayerManager player)
    {
        if (Customer == null)
            return;

        DrinkType drink =
            player.Inventory.CurrentDrink;

        ServeResult result =
            Seat.Order.ServeDrink(drink);

        if (result == ServeResult.EmptyHands)
        {
            ChangeState(
                new EmptyHandsBarSeatState(this));

            return;
        }

        if (result == ServeResult.WrongDrink)
        {
            Customer.Mood.RemoveTolerance(1);

            Customer.Mood.RemoveHappiness(
                Customer.Needs.patienceLossRate);

            if (Customer.Mood.IsAngry || Customer.Mood.IsToleranceDepleted)
            {
                Customer.Blackboard.BlockDecision(
                    BotDecision.GoToBar,
                    10f);

                ChangeState(
                    new LeavingAngryBarSeatState(this));

                return;
            }

            ChangeState(
                new WrongDrinkBarSeatState(this));

            return;
        }

        player.Inventory.ConsumeCurrentDrink();

        player.Wallet.ReceiveDrinkPayment(
            drink,
            Customer);

        Customer.Mood.AddHappiness(
            Customer.Needs.patienceRecoveryRate * 
            GameDataBase.Instance.drinkData.GetSatisfaction(drink));

        if (result == ServeResult.OrderFinished)
        {
            Customer.Needs.thirst -= 0f;
            Customer.Goals.DrinksConsumed++;

            Customer.Blackboard.BlockDecision(
            BotDecision.GoToBar,
            5f);

            ChangeState(
                new LeavingBarSeatState(this));
        }
        else
        {
            ChangeState(
                new ServedBarSeatState(this));
        }
    }

    public bool IsOrderCompleted()
    {
        return Seat.Order.IsCompleted;
    }

    #endregion


    #region State Machine

    public void ChangeState(
        BarSeatState nextState)
    {
        StateMachine.ChangeState(nextState);

        if (nextState == null)
            return;

        if (nextState.CanPlayerInteract)
            EnableInteraction();
        else
            DisableInteraction();

        SetInteraction(nextState.InteractionText);

        InteractableSounds.instance.Play(
            nextState.InteractionSound);

        UpdateCurrentDialogue();

        ShowDialogue();
        ShowThought();
    }

    #endregion
}