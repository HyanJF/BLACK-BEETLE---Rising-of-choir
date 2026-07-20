using UnityEngine;

public class TrashInteraction :
    MonoBehaviour,
    IInteractable
{
    [SerializeField]
    private TrashController controller;

    public bool CanInteract =>
    controller.PlayerInside;

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        controller.PlayerEntered();
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        controller.PlayerExited();
    }

    public void Interact(
        PlayerManager player)
    {
        if (!CanInteract)
            return;

        player.Inventory.RemoveRandomDrink();
    }
}