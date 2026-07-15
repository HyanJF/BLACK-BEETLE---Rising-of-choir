using UnityEngine;

public class BarStockController : MonoBehaviour
{
    #region References

    public BarStock Stock
    {
        get;
        private set;
    }

    #endregion

    #region Variables

    private bool playerInside;

    private float unitPrice;

    #endregion

    #region Initialization

    private void Awake()
    {
        Stock =
            GetComponent<BarStock>();
    }

    private void Start()
    {
        unitPrice =
            GameDataBase.Instance
            .drinkData
            .GetRestockPrice(Stock.Drink);

        Stock.Inventory.OnStockChanged +=
            RefreshUI;
    }

    private void OnDestroy()
    {
        Stock.Inventory.OnStockChanged -=
            RefreshUI;
    }

    #endregion

    #region Player

    public void PlayerEntered()
    {
        playerInside = true;

        Stock.Visuals.SetFocused(true);

        RefreshUI();
    }

    public void PlayerExited()
    {
        playerInside = false;

        Stock.Visuals.SetFocused(false);

        GameDataBase.Instance.stockUI.Hide();
        GameDataBase.Instance.actionUI.Hide();
    }

    public void Interact(
        PlayerManager player)
    {
        if (Stock.Inventory.IsEmpty)
        {
            Restock(player);
            return;
        }

        TakeDrink(player);
    }

    #endregion

    #region Drinks

    private void TakeDrink(
        PlayerManager player)
    {
        if (!player.Inventory.AddDrink(
            Stock.Drink))
            return;

        Stock.Inventory.Consume();
    }

    private void Restock(
        PlayerManager player)
    {
        if (Stock.Inventory.IsFull)
            return;

        float cost =
            GetRestockCost();

        if (!player.Wallet.TrySpendMoney(cost))
            return;

        Stock.Inventory.Restock();
    }

    public float GetRestockCost()
    {
        return unitPrice *
               Stock.Inventory.MaxStock;
    }

    #endregion

    #region UI

    private void RefreshUI()
    {
        if (!playerInside)
            return;

        DrinkData drink =
            GameDataBase.Instance
            .drinkData
            .GetDrink(Stock.Drink);

        GameDataBase.Instance.stockUI.Show();

        GameDataBase.Instance.stockUI.SetDrink(
            drink.icon,
            drink.displayName);

        GameDataBase.Instance.stockUI.SetStock(
            Stock.Inventory.CurrentStock,
            Stock.Inventory.MaxStock);

        GameDataBase.Instance.stockUI.SetPrice(
            GetRestockCost());

        GameDataBase.Instance.actionUI.Show(
            Stock.Inventory.IsEmpty
                ? "Rellenar"
                : "Tomar\nBebida",
            true);

    }

    #endregion
}