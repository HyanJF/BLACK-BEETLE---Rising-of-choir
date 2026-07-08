using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private Image inputImage;

    [SerializeField]
    private TMP_Text actionText;

    [Header("Colors")]
    [SerializeField]
    private Color enabledColor = Color.white;

    [SerializeField]
    private Color disabledColor = Color.gray;

    private void Awake()
    {
        Hide();
    }

    public void Show(string action)
    {
        root.SetActive(true);

        actionText.text = action;
    }

    public void SetInteractable(bool value)
    {
        inputImage.color =
            value ? enabledColor : disabledColor;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}