using System;
using UnityEngine;

[Serializable]
public class DialogueGroup
{
    public DialogueType type;

    [Min(0f)]
    public float duration = 2f;

    [TextArea]
    public string[] dialogues;
}