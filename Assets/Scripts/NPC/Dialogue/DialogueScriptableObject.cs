using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/NPCDialogue", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField] private DialogueStruct[] dialogue;

    // [SerializeField] private DialogueScriptableObject dialogueOption1;
    // [SerializeField] private DialogueScriptableObject dialogueOption2;

    public int GetDialogueLength()
    {
        return dialogue.Length;
    }
    public DialogueStruct GetCurrentDialogue(int index)
    {
        return dialogue[index];
    }
    public string GetDialogueLine(int currentLine)
    {
        return dialogue[currentLine].dialogueLine;
    }
    public AudioClip GetDialogueAudio(int currentLine)
    {
        return dialogue[currentLine].voiceLine;
    }
    public DialogueEventScriptableObject GetDialogueEvent(int currentLine)
    {
        return dialogue[currentLine].dialogueEvent;
    }
}