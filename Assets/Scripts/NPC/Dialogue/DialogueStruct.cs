using System;
using UnityEngine;

[Serializable]
public class DialogueStruct
{
    [TextArea] public string dialogueLine;
    public AudioClip voiceLine;
}
