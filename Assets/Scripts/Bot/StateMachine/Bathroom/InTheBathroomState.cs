using UnityEngine;
using UnityEngine.UIElements;

public class InTheBathroomState : State
{
    private float timer;
    private float waitTime;

    public  InTheBathroomState(
        BotController bot)
        : base(bot)
    { }

    public override void Enter()
    {
        timer = 0f;

        waitTime = Mathf.Lerp(
            2f,
            10f,
            bot.Needs.bladder / 100f
        );

        bot.Thought.DisableThought();
        bot.VisualController.HideBot();
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            WorldBlackboard.Instance
                .Bathroom
                .LeaveBathroom ();

            bot.Mood.happiness += 5f;
            bot.Needs.bladder = 0f;

            bot.StateMachine.ChangeState(
                new ThinkState(bot));
        }
    }

    public override void Exit()
    {
        bot.Thought.Anim(ThoghtType.Happy, 2f);
        bot.VisualController.ShowBot();
    }
}
