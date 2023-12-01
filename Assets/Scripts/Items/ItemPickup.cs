using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public GameObject textPrompt;
    public LayerMask interactableObj;

    public GameObject rightHand;
    public GameObject inFrontOfPlayerPoint;
    public GameObject currentObject;

    GameObject camObj;
    private RaycastHit raycastHit;

    private void Awake()
    {
        camObj = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
    }
    private void OnEnable()
    {
        inputReader.interactEvent += BeginPickup;
        inputReader.leaveInpectionEvent += EndPickup;
    }
    private void OnDisable()
    {
        inputReader.interactEvent -= BeginPickup;
        inputReader.leaveInpectionEvent -= EndPickup;
    }
    void Start()
    {
        textPrompt.SetActive(false);
    }
    private void FixedUpdate()
    {
        InspectUI();
    }
    private void BeginPickup() // Object Pickup
    {
        if(!InspectRaycast())
        return;

        if(currentObject != null)
        return;
        
        currentObject = raycastHit.transform.gameObject;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;
        currentObject.transform.parent = rightHand.transform;
        currentObject.transform.position = rightHand.transform.position;
        currentObject.transform.rotation = rightHand.transform.rotation;
    }
    private void EndPickup()
    {
        // Drop Object
        if (currentObject == null)
        return;

        currentObject.transform.parent = null;
        currentObject.transform.position = inFrontOfPlayerPoint.transform.position;
        currentObject.GetComponent<Rigidbody>().isKinematic = false;
        currentObject = null;
    }
    private void InspectUI()
    {
        bool rayDetected = InspectRaycast();

        if(rayDetected)
        {
            if (currentObject == null)
            {
                textPrompt.SetActive(true);
            }
            // else
            // {
            //     textPrompt.SetActive(false);
            // }
        }
        else
        {
            textPrompt.SetActive(false);
        }
    }
    private GameObject InspectRaycast()
    {
        if(Physics.Raycast(camObj.transform.position, camObj.transform.forward, out raycastHit, 2f, interactableObj, QueryTriggerInteraction.Collide))
        {
            return raycastHit.transform.gameObject;
        }
        return null;
    }
}
