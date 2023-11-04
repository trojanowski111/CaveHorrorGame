using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject textPrompt;
    public LayerMask interactableObj;

    public GameObject rightHand;
    public GameObject inFrontOfPlayerPoint;
    public GameObject currentObject;

    GameObject camObj;

    // Start is called before the first frame update
    void Start()
    {
        textPrompt.SetActive(false);

        camObj = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Drop Object
        if (Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.transform.parent = null;
            currentObject.transform.position = inFrontOfPlayerPoint.transform.position;
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject = null;
        }


        // if (currentObject != null)
        // {
        //     if (currentObject.transform.position != new Vector3(0f,0f,0f))
        //     {
        //         currentObject.transform.position = new Vector3(0f,0f,0f);
        //     }
        // }

        // Object Pickup
        RaycastHit hit;

        if(Physics.Raycast(camObj.transform.position, camObj.transform.forward, out hit, 2f, interactableObj, QueryTriggerInteraction.Collide))
        {
            // if (currentObject == null)
            // {
                textPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentObject = hit.transform.gameObject;
                    currentObject.GetComponent<Rigidbody>().isKinematic = true;
                    currentObject.transform.parent = rightHand.transform;
                    currentObject.transform.position = rightHand.transform.position;
                    currentObject.transform.rotation = rightHand.transform.rotation;
                }
            // }
        }
        else
        {
            textPrompt.SetActive(false);
        }
    }
}
