using UnityEngine;

public class DialogueSequencer : MonoBehaviour
{
    // [Header("Dialogue Scriptable")]
    // [SerializeField] private DialogueScriptableObject dialogueStart;
    // [SerializeField] private DialogueScriptableObject dialogueEnd;
    // private DialogueScriptableObject currentDialogue;

    // [Header("General Components")]
    // [SerializeField] private AudioSource audioSource;
    // [SerializeField] private NPCHeadLook npcHeadLook;
    // [SerializeField] private Transform focusPointForPlayer;

    // private int currentLine = 0;
    // private bool waitingForChoice = false;

    // private void Start()
    // {
    //     currentDialogue = dialogueStart;
    // }
    // public void PlayerClose(Transform playerPos)
    // {
    //     npcHeadLook.LookAt(playerPos);
    //     dialogueCanvas.transform.LookAt(playerPos);

    //     dialogueCanvas.enabled = true;
    //     interactionPrompt.enabled = true;
    // }
    // public void DialogueStarted()
    // {
    //     interactionPrompt.enabled = false;
    //     dialogueText.enabled = true;
    //     PlayDialogue();
    // }
    // public void DialogueLeft()
    // {
    //     dialogueText.enabled = false;
    //     interactionPrompt.enabled = true;

    //     for(int i = 0; i < choiceButtons.Length; i++)
    //     {
    //         choiceButtons[i].gameObject.SetActive(false);
    //     }
    // }
    // public void PlayDialogue()
    // {
    //     if(currentLine < currentDialogue.GetDialogueLength())
    //     {
    //         if(currentDialogue.GetDialogueLine(currentLine) != null)
    //         dialogueText.text = currentDialogue.GetDialogueLine(currentLine);

    //         if(currentDialogue.GetDialogueAudio(currentLine) != null)
    //         audioSource.PlayOneShot(currentDialogue.GetDialogueAudio(currentLine));

    //         if(currentDialogue.GetDialogueEvent(currentLine) != null)
    //         currentDialogue.GetDialogueEvent(currentLine).RaiseEvent();

    //         if(currentDialogue.GetCurrentDialogue(currentLine).choices.Length != 0)
    //         {
    //             for(int i = 0; i < currentDialogue.GetCurrentDialogue(currentLine).choices.Length; i++)
    //             {
    //                 waitingForChoice = true;
    //                 choiceButtons[i].gameObject.SetActive(true);
    //             }
    //         }
    //     }
    // }
    // public void PlayEndDialogue()
    // {
    //     audioSource.Stop();
    //     interactionPrompt.enabled = false;
    //     dialogueText.enabled = true;

    //     int randomNum = Random.Range(0, dialogueEnd.GetDialogueLength());
        
    //     if(currentLine < currentDialogue.GetDialogueLength())
    //     {
    //         if(!dialogueEnd.GetCurrentDialogue(currentLine).allowToSkip)
    //         {
    //             if(audioSource.isPlaying)
    //             return;
    //         }
    //     }

    //     if(dialogueEnd.GetDialogueLine(randomNum) != null)
    //     dialogueText.text = dialogueEnd.GetDialogueLine(randomNum);

    //     if(dialogueEnd.GetDialogueAudio(randomNum) != null)
    //     audioSource.PlayOneShot(dialogueEnd.GetDialogueAudio(randomNum));

    //     if(dialogueEnd.GetDialogueEvent(randomNum) != null)
    //     dialogueEnd.GetDialogueEvent(randomNum).RaiseEvent();
    // }
    // public void UpdateChoiceDialogue(DialogueScriptableObject newDialogueScriptableObject)
    // {
    //     for(int i = 0; i < choiceButtons.Length; i++)
    //     {
    //         choiceButtons[i].gameObject.SetActive(false);
    //     }
    //     currentLine = 0;
    //     currentDialogue = newDialogueScriptableObject;
    //     waitingForChoice = false;
    //     audioSource.Stop();
    //     PlayDialogue();
    // }
    // public void NextDialogue()
    // {
    //     if (currentLine < currentDialogue.GetDialogueLength())
    //     {
    //         if(!currentDialogue.GetCurrentDialogue(currentLine).allowToSkip)
    //         {
    //             if(audioSource.isPlaying)
    //             return;
    //         }
    //     }
    //     audioSource.Stop();
    //     currentLine ++;
    //     PlayDialogue();
    // }
    // public bool IsEndOfDialogue()
    // {
    //     return currentLine >= currentDialogue.GetDialogueLength() - 1;
    // }
    // public void PlayerLeft()
    // {
    //     dialogueCanvas.enabled = false;
    // }
    // public Transform GetFocusPoint()
    // {
    //     return focusPointForPlayer;
    // }
    // public bool IsWaitingForChoice()
    // {
    //     return waitingForChoice;
    // }
}
