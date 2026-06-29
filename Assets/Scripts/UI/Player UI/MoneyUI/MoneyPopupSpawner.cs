using UnityEngine;

public class MoneyPopupSpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private MoneyPopup popupPrefab;

    private void Start()
    {
        player.Wallet.OnMoneyAdded += SpawnGainPopup;
        player.Wallet.OnMoneySpent += SpawnLosePopup;
    }

    private void OnDestroy()
    {
        if (player == null)
            return;

        player.Wallet.OnMoneyAdded -= SpawnGainPopup;
        player.Wallet.OnMoneySpent -= SpawnLosePopup;
    }

    private void SpawnGainPopup(float amount)
    {
        MoneyPopup popup =
            Instantiate(
                popupPrefab,
                transform,
                false);

        popup.Show(amount);
    }

    private void SpawnLosePopup(float amount)
    {
        MoneyPopup popup =
            Instantiate(
                popupPrefab,
                transform,
                false);

        popup.Show(-amount);
    }
}