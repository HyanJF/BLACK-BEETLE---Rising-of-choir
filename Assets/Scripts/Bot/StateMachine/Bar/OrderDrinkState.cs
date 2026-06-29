using UnityEngine;

public class OrderDrinkState : State
{
    public OrderDrinkState(BotController bot)
        : base(bot) { }

    public override void Enter()
    {
        BarSeat seat = bot.Blackboard.ReservedBarSeat;

        bot.Thought.DisableThought();
        bot.VisualController.HideBot();

        seat.ReceiveCustomer(bot);

        seat.Order.OnOrderCompleted += HandleOrderCompleted;
    }

    public override void Update() { }

    public override void Exit()
    {
        BarSeat seat = bot.Blackboard.ReservedBarSeat;

        if (seat != null)
        {
            seat.Order.OnOrderCompleted -= HandleOrderCompleted;

            WorldBlackboard.Instance.Bar.ReleaseSeat(seat);
            bot.Blackboard.ReservedBarSeat = null;
        }

        bot.Mood.happiness += 10f;
        bot.Needs.bladder += 15;
        bot.Needs.thirst = 0f;

        bot.Goals.DrinksConsumed++;

        bot.VisualController.ShowBot();
        bot.Thought.Anim(ThoghtType.Happy, 2f);
    }

    private void HandleOrderCompleted()
    {
        bot.StateMachine.ChangeState(new ThinkState(bot));
    }
}