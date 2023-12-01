using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private LayerMask occlusionLayerMask;
    [SerializeField] private float raycastOffset;
    [SerializeField] private float checkAngle;
    private PlayerController playerController;
    private CameraController cameraController;
    private Interactable currentInteractable;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void OnEnable()
    {
        inputReader.interactEvent += Interact;
    }
    private void OnDisable()
    {
        inputReader.interactEvent -= Interact;
    }
    private void Interact()
    {
        if(InteractableFound(currentInteractable))
        currentInteractable.SetInteractEvent();
    }
    private bool InteractableFound(Interactable interactable)
    {
        Vector3 directionToInteractable = interactable.transform.position - transform.position;
        Gizmos.DrawLine(transform.position + Vector3.up * raycastOffset, directionToInteractable);

        if(!InteractableVisible(directionToInteractable))
        {
            Debug.Log("Not Visible to NPC");
            return false;
        }
        
        Debug.Log("Visible to NPC");
        return true;
    }
    private bool InteractableVisible(Vector3 direction)
    {
        if(Vector3.Angle(direction, transform.forward) > checkAngle || Physics.Raycast(transform.position + Vector3.up * raycastOffset, direction, 10, occlusionLayerMask))
        {
            return false;
        }
        return true;
    }
    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent<Interactable>(out Interactable interactable))  
        return;
        interactable.SetNearbyEvent(transform);
        // InteractableFound(interactable);
    }
    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.TryGetComponent<Interactable>(out Interactable npcDialogue))  
    //     {
    //         Debug.Log("LEFT NPC");
    //     }   
    // }
}
