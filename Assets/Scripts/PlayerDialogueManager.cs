using UnityEngine;

public class PlayerDialogueManager : MonoBehaviour
{
    [SerializeField] private LayerMask occlusionLayerMask;

    private PlayerController playerController;
    private CameraController cameraController;

    private bool inDialogue = false;
    private bool canTalkToNpc;

    private NPCDialogue currentNpc;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void EnterDialogue()
    {
        inDialogue = true;
        playerController.canCrouch = false;
        cameraController.mouseSensitivity = 0;
    }
    private void ExitDialogue()
    {
        inDialogue = false;
        playerController.canCrouch = true;
        cameraController.mouseSensitivity = 250;
    }
    private void Update()
    {
        if(canTalkToNpc)
        {
            if(Input.GetKeyDown("e"))
            {
                EnterDialogue();
                currentNpc.DialogueStarted();
            }
        }
        if(inDialogue)
        {
            cameraController.transform.LookAt(currentNpc.GetFocusPoint());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent<NPCDialogue>(out NPCDialogue npcDialogue) || inDialogue)  
        return;

        Vector3 directionToNpc = npcDialogue.transform.position - transform.position;

        Debug.Log("CLOSE TO NPC");

        Debug.DrawRay(transform.position, directionToNpc);
        if(Physics.Raycast(transform.position, directionToNpc, 10, occlusionLayerMask))
        {
            Debug.Log("Not visible to NPC");
            return;
        }
        npcDialogue.PlayerClose(transform);
        currentNpc = npcDialogue;
        canTalkToNpc = true;
        Debug.Log("Visible to NPC");
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<NPCDialogue>(out NPCDialogue npcDialogue))  
        {
            npcDialogue.PlayerLeft();
            Debug.Log("LEFT NPC");
        }   
    }
}
