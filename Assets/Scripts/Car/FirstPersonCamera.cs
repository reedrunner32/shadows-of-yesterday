using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float sensitivity = 2f;
    public float acceleration = 20f;
    public float deceleration = 15f;
    public float maxSpeed = 5f;
    public Transform playerBody;

    private float xRotation = 0f; // vertical
    private float yRotation = 180f; // horizontal

    private Vector3 velocity = Vector3.zero;
    public bool controlsEnabled = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (SettingsManager.Instance != null)
        {
            sensitivity = SettingsManager.Instance.GetSensitivity();
        }
    }

    void Update()
    {
        if (!controlsEnabled) return;

        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        yRotation = Mathf.Clamp(yRotation, -70f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    public void disableControls()
    {
        controlsEnabled = false;
    }
}
