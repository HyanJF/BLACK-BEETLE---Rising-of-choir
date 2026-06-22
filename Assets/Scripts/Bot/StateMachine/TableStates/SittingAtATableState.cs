using UnityEngine;

public class SittingAtATableState : State
{
    private float timer;
    private const float waitTime = 5f;

    public SittingAtATableState(
        BotController bot)
        : base(bot)
    { }

    public override void Enter()
    {
        timer = 0f;

        Seat seat = 
            bot.Blackboard.ReservedSeat;

        seat.client.SetActive(true);

        bot.VisualController.HideBot();
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            bot.Needs.comfort = 0f;
            bot.Needs.thirst += 20f;

            bot.Goals.SocialActivities++;

            bot.Blackboard.ReservedTable.ReleaseSeat(bot);
            bot.Blackboard.ReservedTable = null;

            bot.StateMachine.ChangeState(new ThinkState(bot));
        }
    }

    public override void Exit()
    {
        Seat seat =
            bot.Blackboard.ReservedSeat;

        seat.client.SetActive(false);

        bot.VisualController.ShowBot();

        bot.Blackboard.ReservedTable?.ReleaseSeat(bot);
    }
}
