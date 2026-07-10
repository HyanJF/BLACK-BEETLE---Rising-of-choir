using Unity.VisualScripting;
using UnityEngine;

public class GoToTheTable : State
{
    public const float NoTablePenalty = 5f;
    public GoToTheTable(
        BotController bot)
        : base(bot)
    {

    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        bot.Blackboard.TargetPlaceType = PlaceType.Table;

        bool foundSeat =
            WorldBlackboard.Instance.ReserveTableSeat(
                bot,
                out Table table,
                out Seat seat);

        if (!foundSeat)
        {
            bot.Mood.RemoveHappiness(
                NoTablePenalty *
                bot.Mood.MoodMultiplier);

            bot.Blackboard.BlockDecision(
                BotDecision.GoToTable, 
                10f);

            bot.Thought.Anim(ThoghtType.Angry, 2f);

            bot.StateMachine.ChangeState(
                new WanderState(bot)
            );

            return;
        }

        bot.Blackboard.ReservedTable =
            table;

        bot.Blackboard.ReservedSeat =
            seat;

        bot.Navigation.NavigateTo(
            seat.location.position);
    }

    public override void Exit()
    {
        bot.Movement.OnDestinationReached -=
            OnDestinationReached;
    }

    private void OnDestinationReached()
    {

        bot.StateMachine.ChangeState(
            new SittingAtATableState(bot)
        );
    }
}
