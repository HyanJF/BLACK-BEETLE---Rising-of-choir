using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RoundTransitionUI : MonoBehaviour
{
    #region References

    [SerializeField]
    private RectTransform panel;

    [SerializeField]
    private CanvasGroup titleGroup;

    [SerializeField]
    private CanvasGroup subtitleGroup;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI subtitleText;

    private Coroutine roundRoutine;

    #endregion

    #region Animation

    [SerializeField]
    private float slideDuration = 0.5f;

    [SerializeField]
    private float messageDuration = 1f;

    [SerializeField]
    private float fadeDuration = 0.25f;

    [SerializeField]
    private float finishDelay = 0.5f;

    #endregion

    #region Data

    private Vector2 center;
    private Vector2 hiddenRight;
    private Vector2 hiddenLeft;

    #endregion

    #region Unity

    private void Awake()
    {
        center =
            panel.anchoredPosition;

        hiddenRight =
            center +
            Vector2.right *
            Screen.width;

        hiddenLeft =
            center +
            Vector2.left *
            Screen.width;

        panel.anchoredPosition =
            hiddenRight;

        titleGroup.alpha = 0f;
        subtitleGroup.alpha = 0f;

        gameObject.SetActive(false);
    }

    #endregion

    #region Public

    public void Play(
        string title,
        string subtitle,
        Action onFinished)
    {
        StopAllCoroutines();

        gameObject.SetActive(true);

        StartCoroutine(
            SequenceRoutine(
                title,
                subtitle,
                onFinished));
    }

    #endregion

    #region Sequence

    private IEnumerator SequenceRoutine(
        string title,
        string subtitle,
        Action onFinished)
    {
        titleText.text = title;
        subtitleText.text = subtitle;

        panel.anchoredPosition =
            hiddenRight;

        titleGroup.alpha = 0f;
        subtitleGroup.alpha = 0f;

        yield return
            Slide(
                hiddenRight,
                center);

        yield return
            FadeCanvas(
                titleGroup,
                0f,
                1f);

        if (!string.IsNullOrWhiteSpace(subtitle))
        {
            yield return
                FadeCanvas(
                    subtitleGroup,
                    0f,
                    1f);
        }

        yield return
            new WaitForSeconds(
                messageDuration);

        yield return
            FadeCanvas(
                titleGroup,
                1f,
                0f);

        yield return
            FadeCanvas(
                subtitleGroup,
                1f,
                0f);

        yield return
            new WaitForSeconds(
                finishDelay);

        yield return
            Slide(
                center,
                hiddenLeft);

        gameObject.SetActive(false);

        onFinished?.Invoke();
    }

    #endregion

    #region Helpers

    private IEnumerator Slide(
        Vector2 from,
        Vector2 to)
    {
        float timer = 0f;

        while (timer < slideDuration)
        {
            timer += Time.deltaTime;

            panel.anchoredPosition =
                Vector2.Lerp(
                    from,
                    to,
                    timer / slideDuration);

            yield return null;
        }

        panel.anchoredPosition = to;
    }

    private IEnumerator FadeCanvas(
        CanvasGroup canvas,
        float from,
        float to)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            canvas.alpha =
                Mathf.Lerp(
                    from,
                    to,
                    timer / fadeDuration);

            yield return null;
        }

        canvas.alpha = to;
    }

    #endregion
}