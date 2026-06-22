using UnityEngine;

public class ThinkState : State
{
    public ThinkState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Brain.Evaluate();

        ExecuteDecision();
    }

    private void ExecuteDecision()
    {
        switch (
            bot.Brain.CurrentDecision
        )
        {
            case BotDecision.Wander:

                bot.StateMachine.ChangeState(
                    new WanderState(bot)
                );

                break;

            case BotDecision.GoToBar:

                bot.StateMachine.ChangeState(
                    new GoToBarState(bot)
                );

                break;

            case BotDecision.GoToBathroom:

                bot.StateMachine.ChangeState(
                    new GoToTheBathroomState(bot)
                );

                break;

            case BotDecision.Exit:

                bot.StateMachine.ChangeState(
                    new GoToTheExitState(bot)
                );

                break;

            case BotDecision.GoToTable:

                bot.StateMachine.ChangeState(
                    new GoToTheTable(bot)
                );

                break;

            default:
                Debug.Log($"el bot eligio {bot.Brain.CurrentDecision} y ahora vagara");
                bot.StateMachine.ChangeState(
                    new WanderState(bot)
                );

                break;

        }
    }
}