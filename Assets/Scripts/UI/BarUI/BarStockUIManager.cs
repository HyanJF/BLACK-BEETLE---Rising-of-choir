using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarStockUIManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField]
    private GameObject panel;

    [Header("UI")]
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI drinkName;

    [SerializeField]
    private Slider stockSlider;

    [SerializeField]
    private Image sliderFill;

    [SerializeField]
    private TextMeshProUGUI stockText;

    [SerializeField]
    private TextMeshProUGUI refillPrice;

    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void SetDrink(
        Sprite sprite,
        string displayName)
    {
        icon.sprite = sprite;
        drinkName.text = displayName;
    }

    public void SetStock(
        int current,
        int max)
    {
        stockSlider.maxValue = max;
        stockSlider.value = current;

        stockText.text =
            $"{current}/{max}";

        float percent =
            (float)current / max;

        sliderFill.color =
            Color.Lerp(
                Color.red,
                Color.green,
                percent);
    }

    public void SetPrice(
        float price)
    {
        refillPrice.text =
            $"${price:0}";
    }
}