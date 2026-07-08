using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    [SerializeField]
    private DialogueCollectionSO[] collections;

    public string GetRandomDialogue(DialogueType type)
    {
        foreach (DialogueCollectionSO collection in collections)
        {
            if (collection.type != type)
                continue;

            return collection.GetRandomDialogue();
        }

        return string.Empty;
    }
}