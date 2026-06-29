using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory Inventory {  get; private set; }
    public PlayerWallet Wallet { get; private set; }
    public PlayerInteraction Interaction { get; private set; }
    public PlayerInputController InputController { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<PlayerInventory>();

        InputController = GetComponent<PlayerInputController>();

        Interaction = GetComponent<PlayerInteraction>();

        Wallet = GetComponent<PlayerWallet>();
    }

}
