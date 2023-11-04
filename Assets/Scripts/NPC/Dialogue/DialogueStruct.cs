using System;
using UnityEngine;

[Serializable]
public class DialogueStruct
{
    public bool allowToSkip = true;
    [TextArea] public string dialogueLine;
    public AudioClip voiceLine;
    public DialogueEventScriptableObject dialogueEvent;
    public DialogueScriptableObject[] choices;
}
