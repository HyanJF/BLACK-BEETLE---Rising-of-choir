using TMPro;
using UnityEngine;

public class MoneyPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float duration = 1.2f;

    [SerializeField]
    private float moveSpeed = 80f;

    [SerializeField]
    private Color gainColor = Color.green;

    [SerializeField]
    private Color loseColor = Color.red;

    [SerializeField]
    private RectTransform rectTransform;

    private float timer;

    public void Show(float amount)
    {
        timer = 0;

        canvasGroup.alpha = 1;

        rectTransform.anchoredPosition =
            new Vector2(
                Random.Range(-50, 50),
                Random.Range(-25, 10));

        text.text =
            amount >= 0
            ? $"+${amount:0}"
            : $"-${-amount:0}";

        text.color =
            amount >= 0
            ? gainColor
            : loseColor;
    }

    private void Update()
    {
        rectTransform.anchoredPosition +=
            Vector2.up *
            moveSpeed *
            Time.deltaTime;

        canvasGroup.alpha =
            Mathf.Lerp(
                1,
                0,
                timer /
                duration);

        timer += Time.deltaTime;

        if (timer >= duration)
            Destroy(gameObject);
    }
}