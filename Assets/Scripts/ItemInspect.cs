using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInspect : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        plPointStartPos = inFrontOfPlayerPoint.transform.localPosition;

        textPrompt.SetActive(false);

        camObj = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        plSc = transform.gameObject.GetComponent<PlayerController>();
        camSc = camObj.gameObject.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        Rotate();
        Zoom();
    }

    void Rotate()
    {
        if (currentObject != null)
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * -rotationSpeed;

            Vector3 right = Vector3.Cross(camObj.transform.up, currentObject.transform.position - camObj.transform.position);
            Vector3 up = Vector3.Cross(currentObject.transform.position - camObj.transform.position, right);

            currentObject.transform.rotation = Quaternion.AngleAxis(-rotX, up) * currentObject.transform.rotation;
            currentObject.transform.rotation = Quaternion.AngleAxis(-rotY, right) * currentObject.transform.rotation;
        }
    }

    void Zoom()
    {
        if (currentObject != null)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && inFrontOfPlayerPoint.transform.localPosition.z < 1f) // forward
            {
                inFrontOfPlayerPoint.transform.localPosition += new Vector3(0f,0f,.1f);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && inFrontOfPlayerPoint.transform.localPosition.z > .5f) // backwards
            {
                inFrontOfPlayerPoint.transform.localPosition += new Vector3(0f,0f,-.1f);
            }
        }
    }

    void Interaction()
    {
        // exit inspection
        if (Input.GetKeyDown(KeyCode.Escape) && currentObject != null)
        {
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

        // pick up
        RaycastHit hit;

        if(Physics.Raycast(camObj.transform.position, camObj.transform.forward, out hit, 2f, inspectableObj, QueryTriggerInteraction.Collide))
        {
            if (currentObject == null)
            {
                textPrompt.SetActive(true);

                // on input with the item
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //set it as current obj
                    currentObject = hit.transform.gameObject;
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
}
