using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/NPCDialogue", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField] private DialogueStruct[] dialogueLines;

    [SerializeField] private DialogueScriptableObject dialogueOption1;
    [SerializeField] private DialogueScriptableObject dialogueOption2;

    public string GetDialogueLine(int currentLine)
    {
        return dialogueLines[currentLine].dialogueLine;
    }
    public AudioClip GetDialogueAudio(int currentLine)
    {
        return dialogueLines[currentLine].voiceLine;
    }
}