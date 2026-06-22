using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    public Waypoint CurrentWaypoint;

    public List<Node> CurrentPath;

    public Transform TargetPlace;

    public PlaceType TargetPlaceType;

    public RouteType CurrentRoute;

    public Table ReservedTable;
    public BarSeat ReservedBarSeat;
    public Seat ReservedSeat;

    [Header("Decision")]

    public Dictionary<BotDecision, float>
        RetryTimes = new();

    public void BlockDecision(
       BotDecision decision,
       float seconds)
    {
        RetryTimes[decision] =
            Time.time + seconds;
    }

    public bool IsDecisionBlocked(
        BotDecision decision)
    {
        if (!RetryTimes.ContainsKey(decision))
            return false;

        if (Time.time >= RetryTimes[decision])
        {
            RetryTimes.Remove(decision);

            return false;
        }

        return true;
    }
}