using UnityEngine;

public class BotBrain : MonoBehaviour
{
    private BotController bot;

    public BotDecision CurrentDecision
    {
        get;
        private set;
    }

    private void Awake()
    {
        bot = GetComponent<BotController>();
    }

    public void Evaluate()
    {
        CurrentDecision =
            DecideNextAction();
    }

    private BotDecision DecideNextAction()
    {
        if (IsUnHappy)
        {
            return BotDecision.Exit;
        }

        if (bot.Goals.VisitCompleted)
        {
            if (IsHappy)
            {
            }

            return BotDecision.Exit;
        }

        if (
            bot.Needs.BladderPercent >=
            bot.CustomerType.bladderThreshold &&
            !bot.Blackboard.IsDecisionBlocked(
                BotDecision.GoToBathroom)
        )
        {
            return BotDecision.GoToBathroom;
        }

        if (
            bot.Needs.ThirstPercent >=
            bot.CustomerType.thirstThreshold &&
            bot.Goals.DrinksConsumed <
            bot.Goals.RequiredDrinks &&
            !bot.Blackboard.IsDecisionBlocked(
                BotDecision.GoToBar)
        )
        {
            return BotDecision.GoToBar;
        }

        if (
            bot.Needs.ComfortPercent >=
            bot.CustomerType.comfortThreshold &&
            bot.Goals.SocialActivities <
            bot.Goals.RequiredSocialActivities &&
            !bot.Blackboard.IsDecisionBlocked(
                BotDecision.GoToTable)
        )
        {
            return BotDecision.GoToTable;
        }

        return BotDecision.Wander;
    }

    public bool IsHappy =>
        bot.Goals.VisitCompleted &&
        bot.Mood.Happiness >= bot.Mood.JoyLimit;

    public bool IsUnHappy =>
        bot.Mood.Happiness <= bot.Mood.AngryLimit;
}