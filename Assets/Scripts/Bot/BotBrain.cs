using Unity.VisualScripting;
using UnityEditor;
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
            return BotDecision.Exit;
        }

        if (
            bot.Needs.bladder > 60 &&
            !bot.Blackboard.IsDecisionBlocked(
                BotDecision.GoToBathroom
            )
        )
        {
            return BotDecision.GoToBathroom;
        }

        if (
            bot.Needs.thirst > 50 &&
            bot.Goals.DrinksConsumed <
            bot.Goals.RequiredDrinks &&
            !bot.Blackboard.IsDecisionBlocked(
                BotDecision.GoToBar
            )
        )
        {
            return BotDecision.GoToBar;
        }

        if (
            bot.Needs.comfort > 30 &&
            bot.Goals.SocialActivities <
            bot.Goals.RequiredSocialActivities &&
            !bot.Blackboard.IsDecisionBlocked(
                BotDecision.GoToTable
            )
        )
        {
            return BotDecision.GoToTable;
        }

        return BotDecision.Wander;
    }

    public bool IsSatisfied =>
        bot.Goals.VisitCompleted &&
        bot.Mood.happiness >= 75f;

    public bool IsUnHappy =>
        bot.Mood.happiness <= 10;
}