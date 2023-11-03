using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("Dialogue Scriptable")]
    [SerializeField] private DialogueScriptableObject dialogueScriptableObject;

    [Header("General Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private NPCHeadLook npcHeadLook;
    [SerializeField] private Transform focusPointForPlayer;

    [Header("UI")]
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactionPrompt;

    private int currentLine = 0;
 
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

        Debug.Log(dialogueScriptableObject.GetDialogueLine(0));
    }
    private void PlayDialogue()
    {
        // audioSource.Stop();
        if(audioSource.isPlaying)
        return;
        audioSource.PlayOneShot(dialogueScriptableObject.GetDialogueAudio(currentLine));
    }
    public void DialogueLeft()
    {
        // currentLine = 0; // idk if keep the progress or reset
        dialogueText.enabled = false;
        interactionPrompt.enabled = true;
    }
    public bool EndOfDialogue()
    {
        return currentLine > dialogueScriptableObject.GetDialogueLine(currentLine).Length - 1;
    }
    public void NextDialogue()
    {
        if(audioSource.isPlaying)
        return;

        if(currentLine < dialogueScriptableObject.GetDialogueLine(currentLine).Length - 1)
        {
            currentLine ++;
            UpdateDialogueText();
            PlayDialogue();
        }
        else
        {
            Debug.Log("REACHED MAX LINES");
        }
    }
    private void UpdateDialogueText()
    {
        dialogueText.text = dialogueScriptableObject.GetDialogueLine(currentLine);
    }
    public void PlayerLeft()
    {
        dialogueCanvas.enabled = false;
    }
    public Transform GetFocusPoint()
    {
        return focusPointForPlayer;
    }
}
