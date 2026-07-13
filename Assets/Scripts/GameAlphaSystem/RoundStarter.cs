using UnityEngine;

public class RoundStarter :
    MonoBehaviour,
    IInteractable
{
    [SerializeField]
    private RoundManager roundManager;

    private bool playerInside;

    private bool used;

    public bool CanInteract =>
        playerInside &&
        !used &&
        !roundManager.IsRoundActive;

    private void OnTriggerEnter2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = true;
    }

    private void OnTriggerExit2D(
        Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = false;
    }

    public void Interact(
        PlayerManager player)
    {
        if (!CanInteract)
            return;

        used = true;

        roundManager.StartRound();
    }

    public void ResetUsed()
    {
        used = false;
    }
}