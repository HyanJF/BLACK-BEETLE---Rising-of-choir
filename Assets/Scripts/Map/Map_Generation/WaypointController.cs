using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    public float cellSize;

    private List<Waypoint> waypoints = new();

    public List<Waypoint> GenerateWaypoints(
    Node[,] grid,
    int width,
    int height,
    int waypointSpacing)
    {
        waypoints.Clear();

        for (int x = 0; x < width; x += waypointSpacing)
        {
            for (int y = 0; y < height; y += waypointSpacing)
            {
                const int maxAttempts = 5;

                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    int randomX = Random.Range(
                        x,
                        Mathf.Min(x + waypointSpacing, width)
                    );

                    int randomY = Random.Range(
                        y,
                        Mathf.Min(y + waypointSpacing, height)
                    );

                    Node node = grid[randomX, randomY];

                    if (!node.walkable)
                        continue;

                    if (node.isReserved)
                        continue;

                    if (node.movementPenalty > 0)
                        continue;

                    waypoints.Add(
                        new Waypoint(node.worldPosition)
                    );

                    break;
                }
            }
        }

        return waypoints;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Waypoint waypoint in waypoints)
        {
            Gizmos.DrawSphere(
                waypoint.position,
                cellSize * 0.3f
            );
        }
    }
}