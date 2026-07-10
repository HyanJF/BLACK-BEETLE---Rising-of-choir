using System;
using UnityEngine;

[Serializable]
public class DialogueGroup
{
    public DialogueType type;

    [TextArea]
    public string[] dialogues;
}