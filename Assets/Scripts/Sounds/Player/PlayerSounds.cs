using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip moneyReceived;

    [SerializeField]
    private AudioClip moneySpent;

    [SerializeField]
    private PlayerWallet wallet;

    private void Awake()
    {
        wallet.OnMoneyAdded += HandleMoneyAdded;
        wallet.OnMoneySpent += HandleMoneySpent;
    }

    private void OnDestroy()
    {
        wallet.OnMoneyAdded -= HandleMoneyAdded;
        wallet.OnMoneySpent -= HandleMoneySpent;
    }

    private void HandleMoneyAdded(float amount)
    {
        GameDataBase.Instance.soundController.PlaySoundPlayer(moneyReceived);
    }

    private void HandleMoneySpent(float amount)
    {
        GameDataBase.Instance.soundController.PlaySoundPlayer(moneySpent);
    }
}