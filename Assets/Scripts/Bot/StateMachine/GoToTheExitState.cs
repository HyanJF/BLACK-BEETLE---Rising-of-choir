using UnityEngine;
using UnityEngine.UIElements;

public class GoToTheExitState : State
{
    public GoToTheExitState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Blackboard.TargetPlaceType = PlaceType.Exit;

        bot.Blackboard.TargetPlace =
            WorldBlackboard.Instance.Exit;

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
            new TemporalState(bot)
        );
    }
}
