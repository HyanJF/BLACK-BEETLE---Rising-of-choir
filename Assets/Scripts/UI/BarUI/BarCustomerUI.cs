using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarCustomerUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField]
    private GameObject root;

    [Header("Happiness")]
    [SerializeField]
    private Slider happinessSlider;

    [Header("Order")]
    [SerializeField]
    private TextMeshProUGUI drinksLeft;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    public void SetOrder(
        int servedDrinks,
        int totalDrinks)
    {
        drinksLeft.text =
            $"{servedDrinks}/{totalDrinks}";
    }

    public void SetHappiness(
        float current,
        float max)
    {
        happinessSlider.maxValue = max;
        happinessSlider.value = current;
    }
}