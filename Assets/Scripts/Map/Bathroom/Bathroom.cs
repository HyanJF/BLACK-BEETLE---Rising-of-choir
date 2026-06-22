using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    public Transform location;
    public Transform door;

    [Header("Bathroom Capacity")]
    public int stalls = 3;

    [Header("Queue")]
    public List<Transform> QueuePoints = new();

    private int occupiedStalls;

    private readonly List<BotController> queue =
        new();

    public bool HasFreeStall =>
        occupiedStalls < stalls;

    public bool EnterBathroom(
        BotController bot)
    {
        if (!HasFreeStall)
            return false;

        occupiedStalls++;

        return true;
    }

    public void LeaveBathroom()
    {
        occupiedStalls =
            Mathf.Max(
                0,
                occupiedStalls - 1
            );
    }

    public bool JoinQueue(
        BotController bot)
    {
        CleanInvalidBots();

        if (queue.Contains(bot))
            return true;

        if (queue.Count >= QueuePoints.Count)
        {
            return false;
        }

        queue.Add(bot);

        UpdateQueuePositions();

        return true;
    }

    public void RemoveFromQueue(
        BotController bot)
    {
        if (!queue.Contains(bot))
            return;

        queue.Remove(bot);

        UpdateQueuePositions();
    }

    public bool IsFirstInQueue(
        BotController bot)
    {
        CleanInvalidBots();

        if (queue.Count == 0)
            return false;

        return queue[0] == bot;
    }

    private void CleanInvalidBots()
    {
        bool changed = false;

        for (
            int i = queue.Count - 1;
            i >= 0;
            i--
        )
        {
            if (
                queue[i] == null ||
                queue[i].StateMachine.CurrentState
                    is not BathroomQueueState
            )
            {

                queue.RemoveAt(i);

                changed = true;
            }
        }

        if (changed)
            UpdateQueuePositions();
    }

    private void UpdateQueuePositions()
    {
        CleanQueueDuplicates();

        for (
            int i = 0;
            i < queue.Count &&
            i < QueuePoints.Count;
            i++
        )
        {
            BotController bot = queue[i];

            if (
                bot.StateMachine.CurrentState
                    is BathroomQueueState
            )
            {
                bot.Navigation.NavigateTo(
                    QueuePoints[i].position
                );
            }
        }
    }

    private void CleanQueueDuplicates()
    {
        HashSet<BotController> seen =
            new();

        for (
            int i = queue.Count - 1;
            i >= 0;
            i--
        )
        {
            if (!seen.Add(queue[i]))
            {
                queue.RemoveAt(i);
            }
        }
    }
}