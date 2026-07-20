using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    [SerializeField]
    private PlayerManager player;

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(
            out var interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(
            out var interactable))
        {
            if (currentInteractable == interactable)
                currentInteractable = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (!context.performed)
            return;

        currentInteractable?.Interact(player);
    }

    public void OnPreviousDrink(
        InputAction.CallbackContext context)
    {

        if (!context.performed)
            return;

        player.Inventory.SelectPrevious();
    }

    public void OnNextDrink(
        InputAction.CallbackContext context)
    {

        if (!context.performed)
            return;

        player.Inventory.SelectNext();
    }
}