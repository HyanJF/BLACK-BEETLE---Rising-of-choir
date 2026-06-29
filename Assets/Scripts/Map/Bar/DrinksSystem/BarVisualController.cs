using System.Collections;
using UnityEngine;

public class BarVisualController : MonoBehaviour
{
    [Header("Customer")]
    [SerializeField] private GameObject client;

    [Header("Order Visual")]
    [SerializeField] private GameObject order;
    [SerializeField] private SpriteRenderer drinkSprite;
    [SerializeField] private Animator drinkAnimator;

    [Header("Colors")]
    [SerializeField] private SpriteRenderer clientSprite;

    [SerializeField] private Color normalColor = Color.black;
    [SerializeField] private Color focusColor = Color.white;
    [SerializeField] private Color servedColor = Color.green;

    [SerializeField]
    private float serveFeedbackTime = 1f;

    private bool focused;
    private Coroutine feedbackRoutine;

    private void Awake()
    {
        Hide();
        HideDrink();

        SetFocused(false);
    }

    public void Show()
    {
        client.SetActive(true);
        order.SetActive(true);
    }

    public void Hide()
    {
        client.SetActive(false);
        order.SetActive(false);
    }

    public void SetFocused(bool value)
    {
        focused = value;

        if (clientSprite != null)
        {
            clientSprite.color =
                focused ?
                focusColor :
                normalColor;
        }

        if (drinkSprite != null)
        {
            drinkSprite.enabled = focused;
        }
    }

    public void ShowDrink(DrinkType drink)
    {
        if (drinkAnimator == null)
            return;

        drinkAnimator.Play(drink.ToString());
    }

    public void HideDrink()
    {
        if (drinkSprite != null)
        {
            drinkSprite.enabled = false;
        }
    }

    public void PlayServeFeedback()
    {
        if (feedbackRoutine != null)
        {
            StopCoroutine(feedbackRoutine);
        }

        feedbackRoutine =
            StartCoroutine(ServeFeedbackRoutine());
    }

    private IEnumerator ServeFeedbackRoutine()
    {
        if (clientSprite == null)
            yield break;

        clientSprite.color = servedColor;

        yield return new WaitForSeconds(
            serveFeedbackTime);

        clientSprite.color =
            focused ?
            focusColor :
            normalColor;

        feedbackRoutine = null;
    }
}