using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class DialogueStruct
{

    [SerializeField] [TextArea] private string line;
    [SerializeField] private AudioClip voiceLine;
}
