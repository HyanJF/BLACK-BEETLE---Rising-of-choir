using UnityEngine;

public class WaitForBarSeatState : State
{
    public WaitForBarSeatState(
    BotController bot)
    : base(bot)
    {
    }

    public override void Enter()
    {
        Bar bar =
            WorldBlackboard.Instance.Bar;

        if (bar.HasFreeSeat)
        {
            bot.Blackboard.ReservedBarSeat =
                bar.ReserveSeat();

            bot.StateMachine.ChangeState(
                new GoToBarSeatState(bot)
            );

            return;
        }

        bot.StateMachine.ChangeState(
            new QueueState(bot)
        );
    }
}