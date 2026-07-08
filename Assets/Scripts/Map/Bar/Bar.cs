using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    #region Inspector

    [Header("Bar")]
    public Transform location;

    [Header("Seats")]
    public List<BarSeat> seats = new();

    [Header("Queue")]
    public List<Transform> QueuePoints = new();

    #endregion

    #region Runtime

    private readonly List<BotController> queue = new();

    #endregion

    #region Queue

    // Agregar un bot en la fila.
    public bool JoinQueue(BotController bot)
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

    // Devuelve true si el bot es el primero de la fila.
    public bool IsFirstInQueue(BotController bot)
    {
        CleanInvalidBots();

        if (queue.Count == 0)
            return false;

        return queue[0] == bot;
    }

    // Elimina un bot de la fila.
    public void RemoveFromQueue(BotController bot)
    {
        if (!queue.Contains(bot))
            return;

        queue.Remove(bot);

        UpdateQueuePositions();
    }

    // Devuelve el punto de espera correspondiente al bot.
    public Transform GetQueuePoint(BotController bot)
    {
        int index = queue.IndexOf(bot);

        if (index < 0 || index >= QueuePoints.Count)
            return null;

        return QueuePoints[index];
    }

    #endregion

    #region Queue Utilities

    // Elimina bots inválidos o que ya abandonaron la cola.
    private void CleanInvalidBots()
    {
        bool changed = false;

        for (int i = queue.Count - 1; i >= 0; i--)
        {
            if (queue[i] == null ||
                queue[i].StateMachine.CurrentState is not QueueState)
            {
                queue.RemoveAt(i);
                changed = true;
            }
        }

        if (changed)
            UpdateQueuePositions();
    }

    // Evita que un bot aparezca dos veces en la fila.
    private void CleanQueueDuplicates()
    {
        HashSet<BotController> seen = new();

        for (int i = queue.Count - 1; i >= 0; i--)
        {
            if (!seen.Add(queue[i]))
            {
                queue.RemoveAt(i);
            }
        }
    }

    // Recalcula las posiciones físicas de toda la fila.
    private void UpdateQueuePositions()
    {
        CleanQueueDuplicates();

        for (int i = 0; i < queue.Count && i < QueuePoints.Count; i++)
        {
            BotController bot = queue[i];

            if (bot.StateMachine.CurrentState is QueueState)
            {
                bot.Navigation.NavigateTo(
                    QueuePoints[i].position);
            }
        }
    }

    #endregion

    #region Seat Reservation
    public bool HasFreeSeat
    {
        get
        {
            foreach (BarSeat seat in seats)
            {
                if (seat.IsFree)
                    return true;
            }

            return false;
        }
    }

    // Reserva el primer asiento libre.
    public BarSeat ReserveSeat()
    {
        foreach (BarSeat seat in seats)
        {
            if (seat.IsFree)
            {
                seat.Reserve();
                return seat;
            }
        }

        return null;
    }

    // Libera un asiento para que pueda volver a reservarse.
    public void ReleaseSeat(BarSeat seat)
    {
        if (seat == null)
            return;

        seat.Free();
    }

    #endregion
}