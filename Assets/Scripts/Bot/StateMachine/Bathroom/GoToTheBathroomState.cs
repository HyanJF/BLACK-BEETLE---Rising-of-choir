using UnityEngine;

public class GoToTheBathroomState : State
{
    public GoToTheBathroomState(BotController bot)
        : base(bot)
    { }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Blackboard.TargetPlaceType = PlaceType.Bathroom;

        bot.Blackboard.TargetPlace = WorldBlackboard.Instance.Bathroom.location;

        bot.Navigation.NavigateTo(
            bot.Blackboard.TargetPlace.position);
    }

    public override void Exit()
    {
        bot.Movement.OnDestinationReached -=
            OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        Bathroom bathroom =
            WorldBlackboard.Instance.Bathroom;

        if (bathroom.EnterBathroom(bot))
        {
            bot.StateMachine.ChangeState(
                new GoToBathroomDoorState(bot)
                );

            return;
        }

        if(bot.Needs.currentPatience <= 10f)
        {
            bot.Mood.RemoveHappiness(
                BathroomQueueState.NoBathroomQueuePenalty *
                bot.Mood.MoodMultiplier);

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

        bot.StateMachine.ChangeState(
            new BathroomQueueState(bot)
        );
    }
}
