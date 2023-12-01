using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask occlusionLayerMask;
    [SerializeField] private float npcRaycastOffset;
    [SerializeField] private float dialogueFov;
    [SerializeField] private float dialogueAngle;
    
    [Header("UI")] // make a new class for dialogue ui
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI interactionPrompt;
    [SerializeField] private Button[] choiceButtons;

    private PlayerController playerController;
    private CameraController cameraController;
    private bool inDialogue = false;
    private NPCDialogue currentNpc;

    private void OnEnable()
    {
        inputReader.interactEvent += TalkToNpc;
        inputReader.skipDialogueEvent += SkipDialogue;
    }
    private void OnDisable()
    {
        inputReader.interactEvent -= TalkToNpc;
        inputReader.skipDialogueEvent -= SkipDialogue;
    }
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void TalkToNpc()
    {
        Debug.Log("INTERACT");
        if(currentNpc == null)
        return;

        if(currentNpc.IsWaitingForChoice())
        return;

        inDialogue = !inDialogue;
        
        if(inDialogue)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            EnterDialogue();

            if(currentNpc.IsEndOfDialogue())
            {
                currentNpc.PlayEndDialogue();
            }
            else
            {
                currentNpc.DialogueStarted();
            }
        }
        else
        {
            if(!currentNpc.IsWaitingForChoice())
            {
                ExitDialogue();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    private void SkipDialogue()
    {
        if(currentNpc == null)
        return;

        if(!inDialogue)
        return;

        if(currentNpc.CanSkipDialogue())
        {
            if(currentNpc.IsEndOfDialogue())
            {
                currentNpc.DialogueLeft();
                ExitDialogue();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if(!currentNpc.IsWaitingForChoice())
            {
                currentNpc.NextDialogue();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent<NPCDialogue>(out NPCDialogue npcDialogue) || inDialogue)  
        return;
        NpcFound(npcDialogue);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<NPCDialogue>(out NPCDialogue npcDialogue))  
        {
            currentNpc = null;
            Debug.Log("LEFT NPC");
        }   
    }
    private void NpcFound(NPCDialogue npcDialogue)
    {
        Vector3 directionToNpc = npcDialogue.transform.position - transform.position;

        Debug.Log("CLOSE TO NPC");

        if(NpcInRange(directionToNpc))
        {
            currentNpc = npcDialogue;
            currentNpc.PlayerClose(transform);

            dialogueCanvas.enabled = true;
            interactionPrompt.enabled = true;

            Debug.Log("Visible to NPC");
        }
        else
        {
            dialogueCanvas.enabled = false;
            currentNpc = null;
        }
    }
    private bool NpcInRange(Vector3 direction)
    {
        if(Vector3.Angle(direction, transform.forward) > dialogueAngle)
        {
            return false;
        }
        if(Physics.Raycast(transform.position + Vector3.up * npcRaycastOffset, direction, 10, occlusionLayerMask))
        {
            return false;
        }
        return true;
    }
    private void EnterDialogue()
    {
        interactionPrompt.enabled = false;
        dialogueText.enabled = true;

        inDialogue = true;
        playerController.SetCanMove(false);
        cameraController.SetCanLook(false);
        cameraController.UpdateFov(dialogueFov, false);
        cameraController.FocusCamera(currentNpc.GetFocusPoint());
    }
    private void ExitDialogue()
    {
        dialogueText.enabled = false;
        interactionPrompt.enabled = true;

        for(int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }

        inDialogue = false;
        playerController.SetCanMove(true);
        cameraController.SetCanLook(true);
        cameraController.UpdateFov(0, true);
        currentNpc.DialogueLeft();
    }
    private void OnDrawGizmos()
    {
        if(!currentNpc)
        return;
        Vector3 directionToNpc = currentNpc.transform.position - transform.position;
        Gizmos.DrawRay(transform.position + Vector3.up * npcRaycastOffset, directionToNpc);
    }
}
