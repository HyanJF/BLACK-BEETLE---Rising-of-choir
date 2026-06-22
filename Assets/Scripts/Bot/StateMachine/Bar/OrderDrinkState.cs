using UnityEngine;

public class OrderDrinkState : State
{
    private float timer;
    private const float waitTime = 10f;

    public OrderDrinkState(
        BotController bot)
        : base(bot)
    {
    }

    public override void Enter()
    {
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            bot.Needs.thirst = 0f;
            bot.Needs.bladder += 10f;

            if(bot.Mood.happiness < bot.Mood.maxHappiness)
            bot.Mood.happiness += 10f;

            bot.Goals.DrinksConsumed++;

            bot.StateMachine.ChangeState(
                new ThinkState(bot)
            );
        }
    }

    public override void Exit()
    {
        if (bot.Blackboard.ReservedBarSeat != null)
        {
            WorldBlackboard.Instance.Bar.ReleaseSeat(
                bot.Blackboard.ReservedBarSeat );

            bot.Blackboard.ReservedBarSeat = null;
        }
    }
}