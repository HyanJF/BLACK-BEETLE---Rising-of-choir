using UnityEngine;

public class BarSeat : MonoBehaviour
{
    #region Seat

    [Header("Seat")]

    [SerializeField]
    private Transform exitPoint;

    public Transform ExitPoint =>
        exitPoint;

    public Transform Location =>
        transform;

    #endregion

    #region Component

    public BarSeatController Controller
    {
        get;
        private set;
    }

    public BarVisualSeat VisualsSeat
    {
        get; 
        private set;
    }

    public BarOrder Order
    {
        get;
        private set;
    }

    public BarThoughtVisual Thought
    {
        get;
        private set;
    }


    #endregion

    #region Availability

    public SeatAvailability Availability
    {
        get;
        private set;
    } = SeatAvailability.Free;

    public bool IsFree =>
        Availability == SeatAvailability.Free;

    public bool IsReserved =>
        Availability == SeatAvailability.Reserved;

    public bool IsOccupied =>
        Availability == SeatAvailability.Occupied;

    #endregion

    #region Methods

    public void Reserve()
    {
        if (!IsFree)
            return;

        Availability =
            SeatAvailability.Reserved;
    }

    public void Occupy()
    {
        if (!IsReserved)
            return;

        Availability =
            SeatAvailability.Occupied;
    }

    public void Free()
    {
        Availability =
            SeatAvailability.Free;
    }

    #endregion

    public void Awake()
    {
        VisualsSeat =
            GetComponent<BarVisualSeat>();

        Controller =
            GetComponent<BarSeatController>();

        Order = 
            GetComponent<BarOrder>();

        Thought =
            GetComponent<BarThoughtVisual>();
    }
}