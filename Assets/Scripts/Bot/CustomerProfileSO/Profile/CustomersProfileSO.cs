using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "CustomerProfile",
    menuName = "Customers/Profile"
)]
public class CustomerProfileSO : ScriptableObject
{
    [Header("Identity")]
    public string displayName;

    public Sprite portrait;

    [Header("Dialogue")]
    public bool useGenericDialogue;

    [TextArea]
    public string description;

    [Header("Dialogues")]
    public List<DialogueGroup> dialogues = new();

    private DialogueGroup GetGroup(DialogueType type)
    {
        foreach (DialogueGroup group in dialogues)
        {
            if (group.type == type)
                return group;
        }

        return null;
    }

    public string GetRandomDialogue(DialogueType type)
    {
        DialogueGroup group = GetGroup(type);

        if (group == null)
            return string.Empty;

        if (group.dialogues == null ||
            group.dialogues.Length == 0)
            return string.Empty;

        return group.dialogues[
            Random.Range(0, group.dialogues.Length)];
    }
}