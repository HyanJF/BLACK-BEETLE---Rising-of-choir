using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    public List<Node> FindPath(
        Vector2 startPos,
        Vector2 targetPos,
        RouteType route)
    {
        Node startNode =
            GridManager.Instance.NodeFromWorld(startPos);

        Node targetNode =
            GridManager.Instance.NodeFromWorld(targetPos);

        ResetNodes();

        startNode.gCost = 0;

        if (!startNode.walkable)
            return null;

        if (!targetNode.walkable)
            return null;

        List<Node> openSet = new();
        HashSet<Node> closedSet = new();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (
                    openSet[i].fCost < currentNode.fCost ||
                    (
                        openSet[i].fCost == currentNode.fCost &&
                        openSet[i].hCost < currentNode.hCost
                    )
                )
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(
                    startNode,
                    targetNode
                );
            }

            foreach (
                Node neighbour
                in GridManager.Instance.GetNeighbours(currentNode)
            )
            {
                if (
                    !neighbour.walkable ||
                    closedSet.Contains(neighbour)
                )
                {
                    continue;
                }

                if (
                    neighbour.isReserved && 
                    neighbour != targetNode)
                {
                    continue;
                }

                int newCostToNeighbour =
                    currentNode.gCost +
                    GetDistance(
                        currentNode,
                        neighbour
                    ) +
                    GetPenaltyCost(
                        neighbour,
                        route
                    );

                if (
                    newCostToNeighbour < neighbour.gCost ||
                    !openSet.Contains(neighbour)
                )
                {
                    neighbour.gCost =
                        newCostToNeighbour;

                    neighbour.hCost =
                        GetDistance(
                            neighbour,
                            targetNode
                        );

                    neighbour.parent =
                        currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private int GetPenaltyCost(
        Node node,
        RouteType route)
    {
        int penalty =
            node.movementPenalty;

        switch (route)
        {
            case RouteType.Shorter:

                if (
                    node.penaltyType.HasFlag(
                        PenaltyType.NearObstacle
                    )
                )
                {
                    penalty = 0;
                }

                break;

            case RouteType.AlittleLonger:

                if (
                    node.penaltyType.HasFlag(
                        PenaltyType.NearObstacle
                    )
                )
                {
                    penalty /= 2;
                }

                break;

            case RouteType.Medium:

                break;

            case RouteType.Long:

                if (
                    node.penaltyType.HasFlag(
                        PenaltyType.NearObstacle
                    )
                )
                {
                    penalty *= 4;
                }

                break;

            case RouteType.VeryLong:

                if (
                    node.penaltyType.HasFlag(
                        PenaltyType.NearObstacle
                    )
                )
                {
                    penalty *= 8;
                }

                if (
                    node.penaltyType.HasFlag(
                        PenaltyType.ReservedArea
                    )
                )
                {
                    penalty *= 8;
                }

                break;
        }

        return penalty;
    }

    private List<Node> RetracePath(
        Node startNode,
        Node endNode)
    {
        List<Node> path = new();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);

            currentNode = currentNode.parent;
        }

        path.Add(startNode);

        path.Reverse();

        return path;
    }

    private int GetDistance(
        Node a,
        Node b)
    {
        int dstX =
            Mathf.Abs(a.gridX - b.gridX);

        int dstY =
            Mathf.Abs(a.gridY - b.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY +
                   10 * (dstX - dstY);
        }

        return 14 * dstX +
               10 * (dstY - dstX);
    }

    private void ResetNodes()
    {
        foreach (Node node in GridManager.Instance.grid)
        {
            node.gCost = int.MaxValue;
            node.hCost = 0;
            node.parent = null;
        }
    }
}