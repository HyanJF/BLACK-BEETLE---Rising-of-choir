using UnityEngine;

public class BarStock : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private DrinkType drink;

    public DrinkType Drink => drink;

    public BarStockController Controller
    {
        get;
        private set;
    }

    public BarStockInventory Inventory
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
        Controller =
            GetComponent<BarStockController>();

        Inventory =
            GetComponent<BarStockInventory>();

        Visuals =
            GetComponent<BarStockVisualController>();
    }
}