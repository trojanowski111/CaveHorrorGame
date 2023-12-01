using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueChoiceController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Choice;
    [SerializeField] private DialogueScriptableObject dialogue;
    [SerializeField] private NPCDialogue dialogueOwner;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(UpdateDialogue);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(UpdateDialogue);
    }
    private void UpdateDialogue()
    {
        dialogueOwner.UpdateChoiceDialogue(dialogue);
    }
}
