using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Variables")]
    public float mouseSensitivity;

    // public Vector3 camPos;
    public Vector3 camOffset;

    [Header("Other")]
    public Transform plBody;
    public Transform plHead;
    public Transform plCamPoint;


    float mouseX;
    float mouseY;

    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = plCamPoint.transform.position + camOffset;

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -60f, 90f);
        plHead.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        plBody.Rotate(Vector3.up * mouseX);
    }
}
