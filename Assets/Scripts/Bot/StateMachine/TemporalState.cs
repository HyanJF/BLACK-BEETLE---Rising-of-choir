public class TemporalState : State
{
    public TemporalState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.FinishVisit();
    }
}