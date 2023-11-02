using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/NPCDialogue", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    [SerializeField] private DialogueStruct[] dialogueLines;
}
