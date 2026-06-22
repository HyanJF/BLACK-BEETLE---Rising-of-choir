using UnityEngine;

public class Node
{
    public bool walkable;
    public bool isReserved;

    public int movementPenalty;

    public PenaltyType penaltyType;

    public Vector2 worldPosition;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public Node parent;

    public int fCost => gCost + hCost;

    public Node(
        bool walkable,
        Vector2 worldPosition,
        int gridX,
        int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;

        movementPenalty = 0;
        penaltyType = PenaltyType.None;
    }
}