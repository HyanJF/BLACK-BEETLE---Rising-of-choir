using UnityEngine;

public class BarStock : MonoBehaviour
{
    [SerializeField]
    private DrinkType drink;

    public DrinkType Drink => drink;

    public BarStockInteraction Interaction
    {
        get;
        private set;
    }

    public BarStockVisualController Visuals
    {
        get;
        private set;
    }

    private void Awake()
    {
        Visuals =
            GetComponent<BarStockVisualController>();

        Interaction =
            GetComponent<BarStockInteraction>();

        Interaction.Initialize(this);
    }

    public void PickupDrink(
        PlayerManager player)
    {
        if (drink == DrinkType.None)
        {
            Debug.LogWarning("Stock sin bebida asignada");
            return;
        }


        if (!player.Inventory.AddDrink(drink))
        {
            Debug.Log("Inventario lleno");
            return;
        }
    }
}