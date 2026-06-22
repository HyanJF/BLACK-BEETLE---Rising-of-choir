using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public Transform location;

    public List<Seat> seats = new();

    private void Awake()
    {

    }

    public bool HasFreeSeat
    {
        get
        {
            foreach (Seat seat in seats)
            {
                if (!seat.IsOccupied)
                    return true;
            }

            return false;
        }
    }

    public int OccupiedSeats
    {
        get
        {
            int count = 0;

            foreach (Seat seat in seats)
            {
                if (seat.IsOccupied)
                    count++;
            }

            return count;
        }
    }

    public Seat ReserveSeat(
    BotController bot)
    {
        foreach (Seat seat in seats)
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
        BotController bot)
    {
        foreach (Seat seat in seats)
        {
            if (seat.Occupant == bot)
            {
                seat.Occupant = null;

                return;
            }
        }
    }
}