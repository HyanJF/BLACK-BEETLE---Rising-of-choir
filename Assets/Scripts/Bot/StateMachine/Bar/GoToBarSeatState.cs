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
                .location
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
        BarSeat seat =
            bot.Blackboard.ReservedBarSeat;

        seat.client.SetActive(true);

        bot.VisualController.HideBot();

        bot.transform.position =
            seat.location.position;

        bot.StateMachine.ChangeState(
            new OrderDrinkState(bot)
        );
    }
}