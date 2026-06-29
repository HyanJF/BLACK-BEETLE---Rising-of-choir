public interface IInteractable
{
    bool CanInteract { get; }

    void Interact(PlayerManager player);
}