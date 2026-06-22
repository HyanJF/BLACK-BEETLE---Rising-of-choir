using UnityEngine;

public class GoToBarState : State
{
    public GoToBarState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Blackboard.TargetPlaceType = PlaceType.Bar;

        bot.Blackboard.TargetPlace =
            WorldBlackboard.Instance.Bar.location;

        bot.Navigation.NavigateTo(
            bot.Blackboard.TargetPlace.position
        );
    }

    public override void Exit()
    {
        bot.Movement.OnDestinationReached -=
            OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        bot.StateMachine.ChangeState(
            new WaitForBarSeatState(bot)
        );
    }
}