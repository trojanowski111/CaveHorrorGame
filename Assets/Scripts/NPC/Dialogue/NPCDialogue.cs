using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Scriptable")]
    [SerializeField] private DialogueScriptableObject dialogueStart;
    [SerializeField] private DialogueScriptableObject dialogueEnd;
    private DialogueScriptableObject currentDialogue;

    [Header("General Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NPCHeadLook npcHeadLook;
    [SerializeField] private Transform focusPointForPlayer;

    [Header("UI")]
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactionPrompt;
    [SerializeField] private Button[] choiceButtons;

    private int currentLine = 0;
    private bool waitingForChoice = false;
    private int randomNumDialogue; // really shit way to do it

    private void Start()
    {
        currentDialogue = dialogueStart;
    }
    public void PlayerClose(Transform playerPos)
    {
        npcHeadLook.LookAt(playerPos);
        dialogueCanvas.transform.LookAt(playerPos);

        dialogueCanvas.enabled = true;
        interactionPrompt.enabled = true;
    }
    public void DialogueStarted()
    {
        interactionPrompt.enabled = false;
        dialogueText.enabled = true;
        PlayDialogue();
    }
    public void DialogueLeft()
    {
        dialogueText.enabled = false;
        interactionPrompt.enabled = true;

        for(int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }
    public void PlayDialogue()
    {
        audioSource.Stop();
        if(currentLine < currentDialogue.GetDialogueLength())
        {
            if(currentDialogue.GetDialogueLine(currentLine) != null)
            dialogueText.text = currentDialogue.GetDialogueLine(currentLine);

            if(currentDialogue.GetDialogueAudio(currentLine) != null)
            audioSource.PlayOneShot(currentDialogue.GetDialogueAudio(currentLine));

            if(currentDialogue.GetDialogueEvent(currentLine) != null)
            currentDialogue.GetDialogueEvent(currentLine).RaiseEvent();

            if(currentDialogue.GetCurrentDialogue(currentLine).choices.Length != 0)
            {
                for(int i = 0; i < currentDialogue.GetCurrentDialogue(currentLine).choices.Length; i++)
                {
                    waitingForChoice = true;
                    choiceButtons[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void PlayEndDialogue()
    {
        audioSource.Stop();
        currentDialogue = dialogueEnd;
        interactionPrompt.enabled = false;
        dialogueText.enabled = true;

        randomNumDialogue = Random.Range(0, currentDialogue.GetDialogueLength());

        if(currentDialogue.GetDialogueLine(randomNumDialogue) != null)
        dialogueText.text = currentDialogue.GetDialogueLine(randomNumDialogue);

        if(currentDialogue.GetDialogueAudio(randomNumDialogue) != null)
        audioSource.PlayOneShot(currentDialogue.GetDialogueAudio(randomNumDialogue));

        if(currentDialogue.GetDialogueEvent(randomNumDialogue) != null)
        currentDialogue.GetDialogueEvent(randomNumDialogue).RaiseEvent();
    }
    public void UpdateChoiceDialogue(DialogueScriptableObject newDialogueScriptableObject)
    {
        for(int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
        currentDialogue = newDialogueScriptableObject;
        currentLine = 0;
        PlayDialogue();
        waitingForChoice = false;
    }
    public void NextDialogue()
    {
        currentLine ++;
        audioSource.Stop();
        PlayDialogue();
    }
    public bool IsEndOfDialogue()
    {
        return currentLine >= currentDialogue.GetDialogueLength() - 1;
    }
    public bool CanSkipDialogue()
    {
        if(currentDialogue == dialogueEnd)
        {
            if(!currentDialogue.GetCurrentDialogue(randomNumDialogue).allowToSkip)
            {
                if(audioSource.isPlaying)
                return false;
            }
            return true;
        }
        else
        {
            if(!currentDialogue.GetCurrentDialogue(currentLine).allowToSkip)
            {
                if(audioSource.isPlaying)
                return false;
            }
            return true;
        }
    }
    public void PlayerLeft()
    {
        dialogueCanvas.enabled = false;
    }
    public Transform GetFocusPoint()
    {
        return focusPointForPlayer;
    }
    public bool IsWaitingForChoice()
    {
        return waitingForChoice;
    }
}
