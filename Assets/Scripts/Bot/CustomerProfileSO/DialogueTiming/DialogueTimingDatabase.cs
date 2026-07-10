using UnityEngine;

public class DialogueTimingDatabase : MonoBehaviour
{
    [SerializeField]
    private DialogueTiming[] timings;

    public float GetDuration(DialogueType type)
    {
        foreach (DialogueTiming timing in timings)
        {
            if (timing.type == type)
                return timing.duration;
        }

        return 0f;
    }
}