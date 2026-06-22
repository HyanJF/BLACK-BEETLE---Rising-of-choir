using UnityEngine;

public class BathroomQueueState : State
{
    public const float NoBathroomQueuePenalty = 8f;

    public BathroomQueueState(BotController bot)
        : base(bot)
    { }

    public override void Enter()
    {
        Bathroom bathroom =
            WorldBlackboard.Instance.Bathroom;

        if (!bathroom.JoinQueue(bot))
        {
            bot.Mood.happiness -=
                NoBathroomQueuePenalty *
                bot.Mood.moodMultiplier;

            bot.Blackboard.BlockDecision(
                BotDecision.GoToBathroom,
                15f);

            bot.StateMachine.ChangeState(
                new WanderState(bot)
            );
        }
    }

    public override void Update()
    {
        Bathroom bathroom =
            WorldBlackboard.Instance.Bathroom;

        if (!bathroom.IsFirstInQueue(bot))
        {
            bot.Needs.currentPatience -=
                Time.deltaTime *
                bot.Needs.patienceLossRate;

            bot.Needs.currentPatience =
                Mathf.Max(0f, bot.Needs.currentPatience);
        }

        if (bot.Needs.currentPatience <= 0f &&
            !bathroom.IsFirstInQueue(bot))
        {
            bathroom.RemoveFromQueue(bot);

            bot.Mood.happiness -=
                NoBathroomQueuePenalty *
                bot.Mood.moodMultiplier;

            bot.Blackboard.BlockDecision(
                BotDecision.GoToBathroom,
                15f
            );

            bot.Thought.Anim(ThoghtType.Angry, 2f);

            bot.StateMachine.ChangeState(
                new WanderState(bot)
            );

            return;
        }

        if (bathroom.IsFirstInQueue(bot) && bathroom.HasFreeStall)
        {

            bathroom.RemoveFromQueue(bot);

            bool entered = bathroom.EnterBathroom(bot);

            if (entered)
            {
                bot.StateMachine.ChangeState(
                    new GoToBathroomDoorState(bot)
                );
            }
        }
    }
}