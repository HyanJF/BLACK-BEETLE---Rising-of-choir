using UnityEngine;

public class OrderDrinkState : State
{
    public OrderDrinkState(BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        BarSeat seat =
            bot.Blackboard.ReservedBarSeat;

        if (seat == null)
            return;

        bot.Thought.DisableThought();
        bot.VisualController.HideBot();

        seat.Controller.OnCustomerFinished +=
            HandleSessionFinished;

        seat.Controller.ReceiveCustomer(bot);
    }

    public override void Exit()
    {
        BarSeat seat =
            bot.Blackboard.ReservedBarSeat;

        if (seat != null)
        {
            seat.Controller.OnCustomerFinished -=
                HandleSessionFinished;
        }

        bot.VisualController.ShowBot();

        if (bot.Mood.IsAngry || 
            bot.Mood.Tolerance == 0)
        {
            bot.Thought.Anim(
                ThoghtType.Angry,
                2f);
        }
        else
        {
            bot.Thought.Anim(
                ThoghtType.Happy,
                2f);
        }

        bot.Mood.SetTolerance(bot.Mood.MaxTolerance);
    }

    private void HandleSessionFinished()
    {
        BarSeat seat =
            bot.Blackboard.ReservedBarSeat;

        if (seat != null)
        {
            seat.Controller.OnCustomerFinished -=
                HandleSessionFinished;
        }

        bot.Blackboard.ReservedBarSeat = null;

        bot.StateMachine.ChangeState(
            new LeaveBarState(bot));
    }
}