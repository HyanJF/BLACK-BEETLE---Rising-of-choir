using UnityEngine;
using System.Collections.Generic;

public class Bar : MonoBehaviour
{
    public Transform location;

    [Header("Bar Seats")]
    public List<BarSeat> seats =
        new();

    [Header("Queue")]
    public List<Transform> QueuePoints =
        new();

    private readonly List<BotController> queue =
        new();

    public bool JoinQueue(
        BotController bot)
    {
        CleanInvalidBots();

        if (queue.Contains(bot))
            return true;

        if (queue.Count >= QueuePoints.Count)
            return false;

        queue.Add(bot);

        UpdateQueuePositions();

        return true;
    }

    public bool IsFirstInQueue(
        BotController bot)
    {
        CleanInvalidBots();

        if (queue.Count == 0)
            return false;

        return queue[0] == bot;
    }

    public void RemoveFromQueue(
        BotController bot)
    {
        if (!queue.Contains(bot))
            return;

        queue.Remove(bot);

        UpdateQueuePositions();
    }

    public Transform GetQueuePoint(
        BotController bot)
    {
        int index =
            queue.IndexOf(bot);

        if (
            index < 0 ||
            index >= QueuePoints.Count
        )
        {
            return null;
        }

        return QueuePoints[index];
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
                    is not QueueState
            )
            {
                queue.RemoveAt(i);
                changed = true;
            }
        }

        if (changed)
        {
            UpdateQueuePositions();
        }
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
            BotController bot =
                queue[i];

            if (
                bot.StateMachine.CurrentState
                    is QueueState
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

    public bool HasFreeSeat
    {
        get
        {
            foreach (BarSeat seat in seats)
            {
                if (!seat.IsOccupied)
                    return true;
            }

            return false;
        }
    }

    public BarSeat ReserveSeat(
        BotController bot)
    {
        foreach (BarSeat seat in seats)
        {
            if (!seat.IsOccupied)
            {
                seat.Occupant = bot;
                return seat;
            }
        }

        return null;
    }

    public void ReleaseSeat(
        BarSeat seat)
    {
        seat.client.SetActive(false);

        seat.Occupant.VisualController.ShowBot();

        seat.Occupant = null;
    }
}