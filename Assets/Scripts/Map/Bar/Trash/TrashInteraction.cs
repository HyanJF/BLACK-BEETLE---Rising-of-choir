using UnityEngine;

public class TrashInteraction :
    MonoBehaviour,
    IInteractable
{

    [SerializeField]
    private bool playerInside;

    public bool CanInteract =>
        playerInside;

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;

    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }

    public void Interact(PlayerManager player)
    {
        if (!CanInteract)
            return;

        if (!player.Inventory.RemoveRandomDrink())
        {
            return;
        }
    }
}