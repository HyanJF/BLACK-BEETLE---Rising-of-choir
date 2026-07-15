using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Root")]
    [SerializeField] private GameObject root;

    [Header("Identity")]
    [SerializeField] private Image portrait;

    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Dialogue")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Dialogue Colors")]
    [SerializeField]
    private Color normalColor = Color.white;

    [SerializeField]
    private Color successColor = Color.green;

    [SerializeField]
    private Color errorColor = Color.red;

    [SerializeField]
    private Color infoColor = Color.cyan;

    private void Awake()
    {
        Hide();
    }

    public void Show(
    BotController customer,
    string dialogue,
    DialogueColorType dialogueColor)
    {
        if (customer == null)
            return;

        CustomerProfileSO profile =
            customer.Profile;

        portrait.sprite =
            profile.portrait;

        nameText.text =
            profile.displayName;

        dialogueText.text =
            dialogue;

        dialogueText.color =
            GetDialogueColor(dialogueColor);

        root.SetActive(true);
    }

    private Color GetDialogueColor(
    DialogueColorType type)
    {
        switch (type)
        {
            case DialogueColorType.Success:
                return successColor;

            case DialogueColorType.Error:
                return errorColor;

            case DialogueColorType.Info:
                return infoColor;

            default:
                return normalColor;
        }
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}