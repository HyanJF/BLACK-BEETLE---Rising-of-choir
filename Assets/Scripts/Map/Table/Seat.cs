using UnityEngine;

public class Seat : MonoBehaviour
{
    public BotController Occupant;

    public Transform location;

    public GameObject client;

    private void Awake()
    {
        if (location == null)
        {
            location = transform;
        }

        if (client != null)
        {
            client.SetActive(false);
        }
    }



    public bool IsOccupied =>
        Occupant != null;
}

