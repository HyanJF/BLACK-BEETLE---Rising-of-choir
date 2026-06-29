using UnityEngine;

public class TrashInteraction :
    MonoBehaviour,
    IInteractable
{
    public GameObject iconTrash;

    [SerializeField]
    private bool playerInside;

    public bool CanInteract =>
        playerInside;

    private void Awake()
    {
        iconTrash.SetActive(false);
    }

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;
            iconTrash.SetActive(true);
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
            iconTrash?.SetActive(false);
    }

    public void Interact(PlayerManager player)
    {
        if (!CanInteract)
            return;

        if (!player.Inventory.RemoveRandomDrink())
        {
            Debug.Log("No tienes bebidas.");
            return;
        }
    }
}