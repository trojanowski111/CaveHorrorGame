using UnityEngine;

public class PlayerDialogueManager : MonoBehaviour
{
    [SerializeField] private LayerMask occlusionLayerMask;
    [SerializeField] private float npcRaycastOffset;

    private PlayerController playerController;
    private CameraController cameraController;

    private bool inDialogue = false;
    private bool canTalkToNpc;

    private NPCDialogue currentNpc;

    [SerializeField] private float dialogueFov;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void Update()
    {
        if(!canTalkToNpc)
        return;

        if(Input.GetKeyDown("e"))
        {
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
        if(inDialogue)
        {
            cameraController.FocusCamera(currentNpc.GetFocusPoint());

            if(Input.GetMouseButtonDown(0))
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
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent<NPCDialogue>(out NPCDialogue npcDialogue) || inDialogue)  
        return;
        NPCFound(npcDialogue);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<NPCDialogue>(out NPCDialogue npcDialogue))  
        {
            npcDialogue.PlayerLeft();
            canTalkToNpc = false;
            Debug.Log("LEFT NPC");
        }   
    }
    private void NPCFound(NPCDialogue npcDialogue)
    {
        Vector3 directionToNpc = npcDialogue.transform.position - transform.position;

        Debug.Log("CLOSE TO NPC");

        if(Physics.Raycast(transform.position + Vector3.up * npcRaycastOffset, directionToNpc, 10, occlusionLayerMask))
        {
            Debug.Log("Not visible to NPC");
            if(!inDialogue)
            canTalkToNpc = false;
            return;
        }
        currentNpc = npcDialogue;
        currentNpc.PlayerClose(transform);
        canTalkToNpc = true;
        Debug.Log("Visible to NPC");
    }
    private void EnterDialogue()
    {
        inDialogue = true;
        playerController.SetCanMove(false);
        cameraController.SetCanLook(false);
        cameraController.UpdateFov(dialogueFov, false);
    }
    private void ExitDialogue()
    {
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
