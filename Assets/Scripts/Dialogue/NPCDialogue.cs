using UnityEngine;
using UnityEngine.Events;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Scriptable")]
    [SerializeField] private DialogueScriptableObject dialogueStart;
    [SerializeField] private DialogueScriptableObject dialogueEnd;
    [SerializeField] private DialogueScriptableObject currentDialogue;

    [Header("General Components")]
    private AIAgent aiAgent;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NPCHeadLook npcHeadLook;
    [SerializeField] private Transform focusPointForPlayer;
    [SerializeField] private UnityEvent dialogueStartedEvent;
    [SerializeField] private UnityEvent dialogueEndedEvent;

    [SerializeField] private AIState dialogueState;

    private bool inDialogue;
    private int currentLine = 0;
    private bool waitingForChoice = false;
    private int randomNumDialogue; // really shit way to do it

    private void Awake()
    {
        aiAgent = GetComponent<AIAgent>();
        currentDialogue = dialogueStart;
    }
    public void PlayerClose(Transform playerPos)
    {
        npcHeadLook.LookAt(playerPos);
    }
    public void DialogueStarted()
    {
        aiAgent.SwitchState(dialogueState);
        inDialogue = true;
        dialogueStartedEvent.Invoke();
        
        PlayDialogue();
    }
    public void DialogueLeft()
    {
        inDialogue = false;
        dialogueEndedEvent.Invoke();
    }
    public void PlayDialogue()
    {
        audioSource.Stop();
        if(currentLine < currentDialogue.GetDialogueLength())
        {
            if(currentDialogue.GetDialogueLine(currentLine) != null)
            // dialogueText.text = currentDialogue.GetDialogueLine(currentLine);

            if(currentDialogue.GetDialogueAudio(currentLine) != null)
            audioSource.PlayOneShot(currentDialogue.GetDialogueAudio(currentLine));

            if(currentDialogue.GetDialogueEvent(currentLine) != null)
            currentDialogue.GetDialogueEvent(currentLine).RaiseEvent();

            if(currentDialogue.GetCurrentDialogue(currentLine).choices.Length != 0)
            {
                for(int i = 0; i < currentDialogue.GetCurrentDialogue(currentLine).choices.Length; i++)
                {
                    waitingForChoice = true;
                    // choiceButtons[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void PlayEndDialogue()
    {
        inDialogue = true;
        audioSource.Stop();
        currentDialogue = dialogueEnd;

        randomNumDialogue = Random.Range(0, currentDialogue.GetDialogueLength());

        if(currentDialogue.GetDialogueLine(randomNumDialogue) != null)
        // dialogueText.text = currentDialogue.GetDialogueLine(randomNumDialogue);

        if(currentDialogue.GetDialogueAudio(randomNumDialogue) != null)
        audioSource.PlayOneShot(currentDialogue.GetDialogueAudio(randomNumDialogue));

        if(currentDialogue.GetDialogueEvent(randomNumDialogue) != null)
        currentDialogue.GetDialogueEvent(randomNumDialogue).RaiseEvent();
    }
    public void UpdateChoiceDialogue(DialogueScriptableObject newDialogueScriptableObject)
    {
        // for(int i = 0; i < choiceButtons.Length; i++)
        // {
        //     choiceButtons[i].gameObject.SetActive(false);
        // }
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
    public Transform GetFocusPoint()
    {
        return focusPointForPlayer;
    }
    public bool IsWaitingForChoice()
    {
        return waitingForChoice;
    }
    public bool InDialogue()
    {
        return inDialogue;
    }
}
