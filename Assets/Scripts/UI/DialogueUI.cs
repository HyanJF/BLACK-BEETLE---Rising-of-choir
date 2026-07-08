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

    private void Awake()
    {
        Hide();
    }

    public void Show(
        BotController customer,
        DialogueType type)
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
            profile.GetRandomDialogue(type);

        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}