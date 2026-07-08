using UnityEngine;

public class BarStockInteraction :
    MonoBehaviour,
    IInteractable
{
    private BarStock stock;

    private bool playerInside;

    public bool CanInteract =>
        playerInside;

    private void Awake()
    {
        stock =
            GetComponent<BarStock>();
    }

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = true;

        stock.Controller.PlayerEntered();
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = false;

        stock.Controller.PlayerExited();
    }

    public void Interact(
        PlayerManager player)
    {
        if (!CanInteract)
            return;

        stock.Controller.Interact(player);
    }
}