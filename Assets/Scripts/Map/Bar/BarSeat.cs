using UnityEngine;

public class BarSeat : MonoBehaviour
{
    public BotController Occupant;

    public GameObject client;

    public Transform Exit;

    public Transform location;

    private void Awake()
    {
        client.gameObject.SetActive(false);
        location = gameObject.GetComponent<Transform>();
    }

    public bool IsOccupied =>
        Occupant != null;
}
