using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebuggerManager : MonoBehaviour
{
    [Header("Grid")]
    public bool drawGrid;

    [Header("Waypoints")]
    public bool drawWaypoints;

    [Header("Navigation")]
    public bool drawPath = true;

    [Header("Bots")]
    public bool drawBotPath = true;


    [Header("State Machine")]
    public bool drawStates;

    public static DebuggerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void DrawBotPath(BotController bot)
    {
        List<Node> path =
            bot.Movement.CurrentPath;

        if (
            path == null ||
            path.Count == 0
        )
        {
            return;
        }

        Node startNode =
            path[0];

        Node endNode =
            path[path.Count - 1];

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(
            startNode.worldPosition,
            0.15f
        );

        Gizmos.color = Color.green;

        Gizmos.DrawSphere(
            endNode.worldPosition,
            0.15f
        );

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(
            bot.transform.position,
            endNode.worldPosition
        );

#if UNITY_EDITOR

        // Nombre en el destino
        Handles.Label(
            endNode.worldPosition +
            Vector2.up * 0.5f,
            bot.name
        );

        // Estado sobre el bot
        if (
            bot.StateMachine != null &&
            bot.StateMachine.CurrentState != null
        )
        {
            Handles.Label(
                bot.transform.position +
                Vector3.up * 1.2f,
                $"{bot.name}\n" +
                $"{bot.StateMachine.CurrentState.GetType().Name}"
            );
        }

#endif
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        if (!drawBotPath)
            return;

        BotController[] bots =
            FindObjectsByType<BotController>(
                FindObjectsSortMode.None
            );

        foreach (BotController bot in bots)
        {
            DrawBotPath(bot);
        }
    }
}