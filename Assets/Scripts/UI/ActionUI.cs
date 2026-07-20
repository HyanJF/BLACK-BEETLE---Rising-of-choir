using TMPro;
using UnityEngine;

public class ActionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

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

    [SerializeField]
    private InputIcon interactIcon;

    private void Start()
    {
        Hide();

        InputManager.Instance.OnInputDeviceChanged += RefreshIcon;

        RefreshIcon();

    }

    private void OnDisable()
    {
        InputManager.Instance.OnInputDeviceChanged -= RefreshIcon;
    }

    public void Show(
        string action,
        bool interactable)
    {

        root.SetActive(true);

        actionText.text = action;

        SetInteractable(interactable);
    }

    public void SetInteractable(
        bool value)
    {
        interactIcon.image.color =
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
        interactIcon.image.sprite =
            InputManager.Instance.GetSprite(interactIcon.action);


    }
}