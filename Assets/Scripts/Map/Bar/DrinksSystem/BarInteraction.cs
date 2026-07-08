using UnityEngine;

public class BarInteraction : MonoBehaviour, IInteractable
{
    #region References

    [SerializeField]
    private BarSeat seat;

    [SerializeField]
    private bool playerInside;

    public bool CanInteract =>
        playerInside;

    #endregion

    #region Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = true;

        seat.VisualsSeat?.SetFocused(true);

        seat.Controller.PlayerEnteredRange();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = false;

        seat.VisualsSeat?.SetFocused(false);

        seat.Controller.PlayerExitedRange();
    }

    #endregion

    #region Interaction

    public void Interact(PlayerManager player)
    {
        if (!playerInside)
            return;

        seat.Controller.OnPlayerInteract(player);
    }

    #endregion
}