using UnityEngine;

public class BarStockInteraction :
    MonoBehaviour,
    IInteractable
{
    private BarStock stock;

    [SerializeField]
    private bool playerInside;

    public bool CanInteract =>
        playerInside;

    public void Initialize(
        BarStock owner)
    {
        stock = owner;
    }

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = true;

        stock.Visuals.SetFocused(true);
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = false;

        stock.Visuals.SetFocused(false);
    }

    public void Interact(
        PlayerManager player)
    {
        if (!CanInteract)
            return;

        stock.PickupDrink(player);
    }
}