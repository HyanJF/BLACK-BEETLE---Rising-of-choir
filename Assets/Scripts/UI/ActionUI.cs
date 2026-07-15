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
    private Color enabledColor =
        Color.white;

    [SerializeField]
    private Color disabledColor =
        Color.gray;

    [SerializeField]
    private InputActionType actionType;

    private void Awake()
    {
        if (InputDeviceManager.Instance != null)
        {
            InputDeviceManager.Instance
                .OnDeviceChanged += RefreshIcon;
        }

        RefreshIcon();

        Hide();
    }

    private void OnDestroy()
    {
        if (InputDeviceManager.Instance != null)
        {
            InputDeviceManager.Instance
                .OnDeviceChanged -= RefreshIcon;
        }
    }

    public void Show(
        string action,
        bool interactable)
    {
        RefreshIcon();

        root.SetActive(true);

        actionText.text = action;

        SetInteractable(interactable);
    }

    public void SetInteractable(
        bool value)
    {
        inputImage.color =
            value
            ? enabledColor
            : disabledColor;
    }

    public void Hide()
    {
        root.SetActive(false);
    }

    private void RefreshIcon()
    {
        if (InputDeviceManager.Instance == null)
            return;

        inputImage.sprite =
            InputDeviceManager.Instance
                .GetSprite(actionType);
    }
}