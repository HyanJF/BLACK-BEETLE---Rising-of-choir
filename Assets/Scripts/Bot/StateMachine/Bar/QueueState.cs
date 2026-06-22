using UnityEngine;

public class QueueState : State
{
    public const float NoBarSeatPenalty = 5f;

    public QueueState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        Bar bar =
            WorldBlackboard.Instance.Bar;

        if (!bar.JoinQueue(bot))
        {
            bot.Mood.happiness -=
                NoBarSeatPenalty *
                bot.Mood.moodMultiplier;

            bot.Blackboard.BlockDecision(
                BotDecision.GoToBar,
                10f);

            bot.StateMachine.ChangeState(
                new WanderState(bot)
            );

            return;
        }

        if (bot.Needs.currentPatience <= 10)
        {
            bot.Needs.currentPatience += 20;
        }
    }

    public override void Update()
    {
        Bar bar =
            WorldBlackboard.Instance.Bar;

        if (!bar.IsFirstInQueue(bot))
        {
            bot.Needs.currentPatience -=
                Time.deltaTime *
                bot.Needs.patienceLossRate;

            bot.Needs.currentPatience =
                Mathf.Max(
                    0f,
                    bot.Needs.currentPatience
                );
        }

        if (
            bot.Needs.currentPatience <= 0 &&
            !bar.IsFirstInQueue(bot)
        )
        {
            bar.RemoveFromQueue(bot);

            bot.Mood.happiness -=
                NoBarSeatPenalty *
                bot.Mood.moodMultiplier;

            bot.Blackboard.BlockDecision(
                BotDecision.GoToBar,
                10f);

            bot.StateMachine.ChangeState(
                new WanderState(bot));

            return;
        }

        if (
            bar.IsFirstInQueue(bot) &&
            bar.HasFreeSeat
        )
        {
            bar.RemoveFromQueue(bot);

            bot.Blackboard.ReservedBarSeat =
                bar.ReserveSeat(bot);

            bot.StateMachine.ChangeState(
                new GoToBarSeatState(bot)
            );
        }
    }
}