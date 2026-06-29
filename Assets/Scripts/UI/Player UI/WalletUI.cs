using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private float countSpeed = 120f;

    private float displayedMoney;

    private void Start()
    {
        displayedMoney =
            player.Wallet.Money;

        RefreshInstant();

        player.Wallet.OnMoneyChanged += Refresh;
    }

    private void OnDestroy()
    {
        if (
            player != null &&
            player.Wallet != null
        )
        {
            player.Wallet.OnMoneyChanged -= Refresh;
        }
    }

    private void Update()
    {
        float target =
            player.Wallet.Money;

        if (Mathf.Approximately(displayedMoney, target))
            return;

        displayedMoney =
            Mathf.MoveTowards(
                displayedMoney,
                player.Wallet.Money,
                countSpeed *
                Time.deltaTime);

        UpdateText();
    }

    private void Refresh()
    {
    }

    private void RefreshInstant()
    {
        displayedMoney =
            player.Wallet.Money;

        UpdateText();
    }

    private void UpdateText()
    {
        moneyText.text =
            $"${displayedMoney:0.00}";
    }
}