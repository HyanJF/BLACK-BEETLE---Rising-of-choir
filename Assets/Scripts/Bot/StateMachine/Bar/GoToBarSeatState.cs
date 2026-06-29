using UnityEngine;

public class GoToBarSeatState : State
{
    public GoToBarSeatState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Navigation.NavigateTo(
            bot.Blackboard
                .ReservedBarSeat
                .Location
                .position
        );
    }

    public override void Exit()
    {
        bot.Movement.OnDestinationReached -=
            OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        bot.transform.position =
            bot.Blackboard
                .ReservedBarSeat
                .Location.position;

        bot.StateMachine.ChangeState(
            new OrderDrinkState(bot)
        );
    }
}