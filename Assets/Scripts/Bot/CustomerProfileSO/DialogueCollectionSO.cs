using UnityEngine;

[CreateAssetMenu(
    fileName = "DialogueCollection",
    menuName = "Dialogue/Collection")]
public class DialogueCollectionSO : ScriptableObject
{
    public DialogueType type;

    [TextArea]
    public string[] dialogues;

    public string GetRandomDialogue()
    {
        if (dialogues == null ||
            dialogues.Length == 0)
            return string.Empty;

        return dialogues[
            Random.Range(0, dialogues.Length)];
    }
}