using System.Collections.Generic;
using UnityEngine;

public class WorldBlackboard : MonoBehaviour
{
    public static WorldBlackboard Instance;
    public Node[,] Grid =>
        GridManager.Instance.grid;
    public List<Waypoint> Waypoints =>
        GridManager.Instance.waypoints;

    [Header("Places")]
    public Bar Bar;
    public List<Table> Tables;
    public Bathroom Bathroom;
    public Transform Exit; 

    private void Awake()
    {
        Instance = this;
    }

    public bool ReserveTableSeat(
    BotController bot,
    out Table table,
    out Seat seat)
    {
        foreach (Table currentTable in Tables)
        {
            seat =
                currentTable.ReserveSeat(bot);

            if (seat != null)
            {
                table = currentTable;
                return true;
            }
        }

        table = null;
        seat = null;

        return false;
    }
}