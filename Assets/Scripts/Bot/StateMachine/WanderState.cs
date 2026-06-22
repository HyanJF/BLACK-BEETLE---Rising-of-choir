using UnityEngine;

public class WanderState : State
{
    private int turns;
    private int maxTurns = 1;

    public WanderState(BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        bot.Movement.OnDestinationReached +=
            OnDestinationReached;

        turns = 0;

        PickNewDestination();
    }

    public override void Update()
    {

        bot.Needs.currentPatience +=
            Time.deltaTime *
            bot.Needs.patienceRecoveryRate;
        
        bot.Needs.currentPatience =
            Mathf.Clamp(
            bot.Needs.currentPatience,
            0,
            bot.Needs.maxPatience
            );
    }

    public override void Exit()
    {
        bot.Movement.OnDestinationReached -=
            OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        turns++;

        if (turns >= maxTurns)
        {
            bot.Needs.comfort += 10;

            if(bot.Needs.currentPatience < bot.Needs.currentPatience)
            bot.Needs.currentPatience += 5;

            bot.StateMachine.ChangeState(
                new ThinkState(bot)
            );

            return;
        }

        PickNewDestination();
    }

    private void PickNewDestination()
    {
        if (WorldBlackboard.Instance.Waypoints == null)
            return;

        if (WorldBlackboard.Instance.Waypoints.Count == 0)
            return;

        int randomIndex =
            Random.Range(
                0,
                WorldBlackboard.Instance.Waypoints.Count
            );

        Waypoint waypoint =
            WorldBlackboard.Instance.Waypoints[randomIndex];

        bot.Blackboard.CurrentWaypoint =
            waypoint;

        bot.Navigation.NavigateTo(
            waypoint.position
        );
    }
}