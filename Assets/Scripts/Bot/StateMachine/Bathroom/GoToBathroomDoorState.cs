using UnityEngine;

public class GoToBathroomDoorState : State
{
    public GoToBathroomDoorState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Navigation.NavigateTo(
            WorldBlackboard.Instance
                .Bathroom
                .door
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
        bot.StateMachine.ChangeState(
            new InTheBathroomState(bot)
        );
    }
}