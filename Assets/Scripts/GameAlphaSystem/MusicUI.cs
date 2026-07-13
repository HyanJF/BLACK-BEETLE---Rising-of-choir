using System.Collections;
using TMPro;
using UnityEngine;

public class MusicRoundUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject root;

    [SerializeField]
    private TextMeshProUGUI songNameText;

    [SerializeField]
    private float visibleTime = 3f;

    [SerializeField]
    private float fadeDuration = 1f;

    private Coroutine fadeRoutine;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        root.SetActive(false);
    }

    public void Show(string songName)
    {
        songNameText.text = songName;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        root.SetActive(true);

        canvasGroup.alpha = 1f;

        fadeRoutine =
            StartCoroutine(
                FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return
            new WaitForSeconds(
                visibleTime);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            canvasGroup.alpha =
                Mathf.Lerp(
                    1f,
                    0f,
                    timer / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = 0f;

        root.SetActive(false);

        fadeRoutine = null;
    }
}