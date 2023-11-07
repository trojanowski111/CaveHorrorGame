using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueTextBoxController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactionPrompt;
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private Transform choicesBoxTransform;
    private bool listenToInput = false;

    private void Awake()
    {
        gameObject.SetActive(false);
        choicesBoxTransform.gameObject.SetActive(false);
    }
    private void OnDialogueStart(DialogueScriptableObject dialogue)
    {
        gameObject.SetActive(true);

        // m_DialogueText.text = node.DialogueLine.Text;
        // m_SpeakerText.text = node.DialogueLine.Speaker.CharacterName;
    }
    private void OnDialogueEnd(DialogueScriptableObject dialogue)
    {
        gameObject.SetActive(false);
        choicesBoxTransform.gameObject.SetActive(false);
        // m_DialogueText.text = node.DialogueLine.Text;
        // m_SpeakerText.text = node.DialogueLine.Speaker.CharacterName;
    }
}
