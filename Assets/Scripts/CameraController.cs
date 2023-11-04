using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Components")]
    private Camera _camera;

    [Header("Variables")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector3 camOffset;
    private bool canLook = true;

    [Header("Other")]
    [SerializeField] private Transform plBody;
    [SerializeField] private Transform plHead;
    [SerializeField] private Transform plCamPoint;
    private float yRotation;

    private float defaultFov;

    private void Awake()
    {
        _camera = Camera.main;    
    }
    void Start()
    {
        defaultFov = _camera.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(!canLook)
        return;

        transform.position = plCamPoint.transform.position + camOffset;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -60f, 90f);
        plHead.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        plBody.Rotate(Vector3.up * mouseX);
    }
    public void UpdateFov(float newFov, bool normalFov)
    {
        if(normalFov)
        {
            _camera.fieldOfView = defaultFov;
            return;
        }
        _camera.fieldOfView = newFov;
    }
    public void FocusCamera(Transform focusTarget)
    {
        transform.LookAt(focusTarget);
    }
    public void SetCanLook(bool newCanLook)
    {
        canLook = newCanLook;
    }
}
