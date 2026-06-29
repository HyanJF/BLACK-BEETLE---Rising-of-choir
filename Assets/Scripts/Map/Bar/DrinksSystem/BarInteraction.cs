using UnityEngine;

public class BarInteraction :
    MonoBehaviour,
    IInteractable
{
    private BarSeat seat;

    [SerializeField]
    private bool playerInside;

    public bool CanInteract =>
        playerInside &&
        seat != null &&
        seat.IsOccupied;

    public void Initialize(
        BarSeat owner)
    {
        seat = owner;
    }

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = true;

        seat.Visuals.SetFocused(true);
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = false;

        seat.Visuals.SetFocused(false);
    }

    public void Interact(
        PlayerManager player)
    {
        if (!CanInteract)
            return;

        seat.Interact(player);
    }

    public void ResetInteraction()
    {
        playerInside = false;

        if (seat != null)
            seat.Visuals.SetFocused(false);
    }
}