using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceButton : MonoBehaviour
{
    [SerializeField] private DialogueScriptableObject dialogue;
    [SerializeField] private NPCDialogue dialogueOwner;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(UpdateDialogue);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(UpdateDialogue);
    }
    private void UpdateDialogue()
    {
        dialogueOwner.UpdateChoiceDialogue(dialogue);
    }
    public void SetDialogueOwner(NPCDialogue npcDialogue)
    {
        dialogueOwner = npcDialogue;
    }
}
