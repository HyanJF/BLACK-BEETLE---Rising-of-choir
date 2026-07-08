public class LeaveBarState : State
{
    public LeaveBarState(BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Navigation.NavigateTo(
            WorldBlackboard.Instance.Bar.location.position);
    }

    public override void Exit()
    {
        bot.Movement.OnDestinationReached -=
            OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        bot.StateMachine.ChangeState(
            new ThinkState(bot));
    }
}