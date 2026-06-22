using System.Collections.Generic;
using UnityEngine;

public class BotNavigation : MonoBehaviour
{
    private Pathfinder pathfinder;
    private BotMovement movement;
    private BotController bot;

    private void Awake()
    {
        movement =
            GetComponent<BotMovement>();

        bot = 
            GetComponent<BotController>();

        pathfinder =
            new Pathfinder();
    }

    public void NavigateTo(
    Vector2 destination)
    {
        RouteType route =
            SelectRoute();

        List<Node> path =
            pathfinder.FindPath(
                transform.position,
                destination,
                route
            );

        if (path == null || path.Count == 0)
        {
            return;
        }

        movement.FollowPath(path);
    }

    private RouteType SelectRoute()
    {
        RouteType newRoute;

        do
        {
            newRoute =
                (RouteType)Random.Range(
                    0,
                    System.Enum.GetValues(
                        typeof(RouteType)
                    ).Length
                );

        } while (
            newRoute ==
            bot.Blackboard.CurrentRoute
        );

        bot.Blackboard.CurrentRoute =
            newRoute;

        return newRoute;
    }
}