using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputReader inputReader;
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

    private void OnEnable()
    {
        inputReader.cameraMoveEvent += MoveCamera;
    }
    private void OnDisable()
    {
        inputReader.cameraMoveEvent -= MoveCamera;
    }
    private void Awake()
    {
        _camera = Camera.main;    
    }
    void Start()
    {
        defaultFov = _camera.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        // MoveCamera(cameraInput);
        transform.position = plCamPoint.transform.position + camOffset;
    }
    private void MoveCamera(Vector2 lookInput)
    {
        if(!canLook)
        return;

        // transform.position = plCamPoint.transform.position + camOffset;

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;
        
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
