using System;
using UnityEngine;

[Serializable]
public class DialogueTiming
{
    public DialogueType type;

    [Min(0f)]
    public float duration = 2f;
}