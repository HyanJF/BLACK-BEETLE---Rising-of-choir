using UnityEngine;

public class TrashInteraction :
    MonoBehaviour,
    IInteractable
{
    public GameObject iconTrash;
    public GameObject inputTrash;

    [SerializeField]
    private bool playerInside;

    public bool CanInteract =>
        playerInside;

    private void Awake()
    {
        iconTrash.SetActive(false);
        inputTrash.SetActive(false);
    }

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;
            iconTrash.SetActive(true);
            inputTrash.SetActive(true);

    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
            iconTrash?.SetActive(false);
            inputTrash?.SetActive(false);
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