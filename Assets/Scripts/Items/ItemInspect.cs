using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspect : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public float rotationSpeed;

    [Header("Components")]
    public GameObject textPrompt;
    public LayerMask inspectableObj;
    public GameObject inFrontOfPlayerPoint;
    Vector3 plPointStartPos;

    public GameObject currentObject;
    Vector3 startPos;
    Quaternion startRot;

    GameObject camObj;
    PlayerController plSc;
    CameraController camSc;
    private RaycastHit raycastHit;

    private void OnEnable()
    {
        inputReader.interactEvent += BeginInteraction;
        inputReader.cameraMoveEvent += Rotate;
        inputReader.inspectionZoomEvent += Zoom;
        inputReader.leaveInpectionEvent += EndInteraction;
    }
    private void OnDisable()
    {
        inputReader.interactEvent -= BeginInteraction;
        inputReader.cameraMoveEvent -= Rotate;
        inputReader.inspectionZoomEvent -= Zoom;
        inputReader.leaveInpectionEvent -= EndInteraction;
    }
    private void Awake()
    {
        camObj = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        plSc = transform.gameObject.GetComponent<PlayerController>();
        camSc = camObj.gameObject.GetComponent<CameraController>();
    }
    void Start()
    {
        plPointStartPos = inFrontOfPlayerPoint.transform.localPosition;
        textPrompt.SetActive(false);
    }

    void FixedUpdate()
    {
        InspectUI();
    }
    private void BeginInteraction() // on input with the item
    {
        if(!InspectRaycast())
        return;

        if(currentObject != null)
        return;
        
        // begin inspection
        currentObject = raycastHit.transform.gameObject;
        currentObject.transform.parent = inFrontOfPlayerPoint.transform;

        // store value for start position & rotation
        startPos = currentObject.transform.position;
        startRot = currentObject.transform.rotation;
        // put it in front of the player / eye view
        currentObject.transform.position = inFrontOfPlayerPoint.transform.position;
        // disable movement etc
        plSc.allowCrouch = false;   
        plSc.allowSprint = false;
        plSc.SetCanMove(false);
        camSc.SetCanLook(false);
    }
    private void EndInteraction()
    {
        // exit inspection
        if (currentObject == null)
        return;

        // put it back in starting position & rotation
        currentObject.transform.position = startPos;
        currentObject.transform.rotation = startRot;
        currentObject.transform.parent = null;
        currentObject = null;

        inFrontOfPlayerPoint.transform.localPosition = plPointStartPos;

        // enable movement etc
        plSc.allowCrouch = true;   
        plSc.allowSprint = true;
        plSc.SetCanMove(true);
        camSc.SetCanLook(true);
    }
    void Rotate(Vector2 cameraInput)
    {
        if (currentObject == null)
        return;

        float rotX = cameraInput.x * rotationSpeed;
        float rotY = cameraInput.y * -rotationSpeed;

        Vector3 right = Vector3.Cross(camObj.transform.up, currentObject.transform.position - camObj.transform.position);
        Vector3 up = Vector3.Cross(currentObject.transform.position - camObj.transform.position, right);

        currentObject.transform.rotation = Quaternion.AngleAxis(-rotX, up) * currentObject.transform.rotation;
        currentObject.transform.rotation = Quaternion.AngleAxis(-rotY, right) * currentObject.transform.rotation;
    }
    void Zoom(Vector2 zoomInput)
    {
        if(currentObject == null)
        return;

        if (zoomInput.y < 0f && inFrontOfPlayerPoint.transform.localPosition.z < 1f) // forward
        {
            inFrontOfPlayerPoint.transform.localPosition += new Vector3(0f,0f,.1f);
        }
        else if (zoomInput.y > 0f && inFrontOfPlayerPoint.transform.localPosition.z > .5f) // backwards
        {
            inFrontOfPlayerPoint.transform.localPosition += new Vector3(0f,0f,-.1f);
        }
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
            else
            {
                textPrompt.SetActive(false);
            }
        }
        else
        {
            textPrompt.SetActive(false);
        }
    }
    private GameObject InspectRaycast()
    {
        if(Physics.Raycast(camObj.transform.position, camObj.transform.forward, out raycastHit, 2f, inspectableObj, QueryTriggerInteraction.Collide))
        {
            return raycastHit.transform.gameObject;
        }
        return null;
    }
}
