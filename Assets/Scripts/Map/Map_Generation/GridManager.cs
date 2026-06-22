using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    public int width = 20;
    public int height = 30;
    public float cellSize = 1f;

    public Node[,] grid;
    public List<Waypoint> waypoints = new();

    public WaypointController waypointC_S;

    [Range(1, 8)]
    public int waypointSpacing;

    public static GridManager Instance;

    [Header("Detection")]
    public LayerMask obstacleLayer;
    public LayerMask reservedAreaLayer;

    private void Awake()
    {
        Instance = this;
        CreateGrid();

        CalculatePenaltyMap();
        CalculateReservedAreaPenalty();

        waypointC_S.cellSize = cellSize;
        waypoints = waypointC_S.GenerateWaypoints(
            grid,
            width,
            height,
            waypointSpacing
        );

    }

    void CreateGrid()
    {
        grid = new Node[width, height];

        Vector2 origin =
            (Vector2)transform.position
            - new Vector2(
                width * cellSize / 2f,
                height * cellSize / 2f
            );

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 worldPos =
                    origin +
                    new Vector2(
                        x * cellSize + cellSize / 2f,
                        y * cellSize + cellSize / 2f
                    );

                bool walkable =
                    !Physics2D.OverlapCircle(
                        worldPos,
                        cellSize * 0.4f,
                        obstacleLayer
                    );

                bool reserved =
                    Physics2D.OverlapCircle(
                        worldPos,
                        cellSize * 0.4f,
                        reservedAreaLayer
                    );

                grid[x, y] = new Node(
                    walkable,
                    worldPos,
                    x,
                    y
                );

                grid[x, y].isReserved = reserved;
            }
        }
    }

    private void CalculatePenaltyMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = grid[x, y];

                if (!node.walkable)
                    continue;

                int penalty = 0;

                for (int checkX = -1; checkX <= 1; checkX++)
                {
                    for (int checkY = -1; checkY <= 1; checkY++)
                    {
                        int nx = x + checkX;
                        int ny = y + checkY;

                        if (
                            nx < 0 ||
                            nx >= width ||
                            ny < 0 ||
                            ny >= height
                        )
                        {
                            continue;
                        }

                        if (!grid[nx, ny].walkable)
                        {
                            penalty += 20;
                        }
                    }
                }

                if (penalty > 0)
                {
                    node.movementPenalty += penalty;

                    node.penaltyType |=
                        PenaltyType.NearObstacle;
                }
            }
        }
    }

    private void CalculateReservedAreaPenalty()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = grid[x, y];

                if (!node.walkable)
                    continue;

                if (!node.isReserved)
                    continue;

                node.movementPenalty += 10;

                node.penaltyType |=
                    PenaltyType.ReservedArea;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;

                        if (
                            nx < 0 ||
                            nx >= width ||
                            ny < 0 ||
                            ny >= height
                        )
                        {
                            continue;
                        }

                        Node neighbour =
                            grid[nx, ny];

                        if (
                            neighbour == node ||
                            !neighbour.walkable
                        )
                        {
                            continue;
                        }

                        neighbour.movementPenalty += 5;

                        neighbour.penaltyType |=
                            PenaltyType.ReservedArea;
                    }
                }
            }
        }
    }

    public Node NodeFromWorld(Vector2 worldPos)
    {
        Vector2 origin =
            (Vector2)transform.position
            - new Vector2(
                width * cellSize / 2f,
                height * cellSize / 2f
            );

        int x = Mathf.FloorToInt(
            (worldPos.x - origin.x) / cellSize
        );

        int y = Mathf.FloorToInt(
            (worldPos.y - origin.y) / cellSize
        );

        x = Mathf.Clamp(x, 0, width - 1);
        y = Mathf.Clamp(y, 0, height - 1);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new();

        int[,] directions =
        {
        { 0,  1 },
        { 0, -1 },
        { 1,  0 },
        {-1,  0 },

        { 1,  1 },
        { 1, -1 },
        {-1,  1 },
        {-1, -1 }
    };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];

            int checkX = node.gridX + dx;
            int checkY = node.gridY + dy;

            if (
                checkX < 0 ||
                checkX >= width ||
                checkY < 0 ||
                checkY >= height
            )
                continue;

            if (dx != 0 && dy != 0)
            {
                Node horizontal =
                    grid[node.gridX + dx, node.gridY];

                Node vertical =
                    grid[node.gridX, node.gridY + dy];

                if (
                    !horizontal.walkable ||
                    !vertical.walkable
                )
                {
                    continue;
                }
            }

            neighbours.Add(
                grid[checkX, checkY]
            );
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
            return;

        foreach (Node node in grid)
        {
            if (!node.walkable)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawCube(
                    node.worldPosition,
                    Vector3.one * cellSize * 0.2f
                );

                continue;
            }

            if (node.isReserved)
            {
                Gizmos.color = Color.cyan;
            }
            else
            {
                float t =
                    Mathf.Clamp01(
                        node.movementPenalty / 100f
                    );

                Gizmos.color =
                    Color.Lerp(
                        Color.gray,
                        Color.yellow,
                        t
                    );
            }

            Gizmos.DrawWireCube(
                node.worldPosition,
                Vector3.one * cellSize
            );

#if UNITY_EDITOR
            if (node.movementPenalty > 0)
                        {
                            Handles.Label(
                                node.worldPosition + Vector2.up * 0.15f,
                                node.movementPenalty.ToString());
                        }
            #endif
        }
    }
}
