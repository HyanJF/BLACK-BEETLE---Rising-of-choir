using UnityEngine;

public class BarSeat : MonoBehaviour
{
    [Header("Seat")]
    public BotController Occupant;

    [SerializeField] private Transform exit;
    public Transform Exit => exit;

    public Transform Location { get; private set; }

    public bool IsOccupied => Occupant != null;

    public BarOrder Order { get; private set; }
    public BarInteraction Interaction { get; private set; }
    public BarVisualController Visuals { get; private set; }

    private void Awake()
    {
        Location = transform;

        Order = GetComponent<BarOrder>();
        Visuals = GetComponent<BarVisualController>();
        Interaction = GetComponentInChildren<BarInteraction>();

        Interaction.Initialize(this);
    }

    public void ReceiveCustomer(BotController bot)
    {
        Occupant = bot;

        Visuals.Show();

        int drinks =
            Random.Range(
                bot.MinDrinksPerOrder,
                bot.MaxDrinksPerOrder + 1
            );

        Order.CreateOrder(bot, drinks);
    }

    public void Leave()
    {
        Order.ClearOrder();
        Visuals.Hide();
        Occupant = null;
    }

    public void Interact(PlayerManager player)
    {
        if (!IsOccupied)
            return;

        DrinkType servedDrink =
            player.Inventory.CurrentDrink;

        BotController customer = Occupant;

        if (Order.ServeDrink(servedDrink))
        {
            player.Inventory.ConsumeCurrentDrink();

            player.Wallet.ReceiveDrinkPayment(
                servedDrink,
                customer);
        }
        else
        {
            Debug.Log("Pedido incorrecto");
        }
    }

    public void SetFocused(bool value)
    {
        Visuals.SetFocused(value);
    }
}