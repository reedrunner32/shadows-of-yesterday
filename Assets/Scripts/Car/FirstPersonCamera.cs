using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float sensitivity = 2f;
    public float moveSpeed = 5f;
    public Transform playerBody;

    private float xRotation = 0f; // vertical
    private float yRotation = 180f; // horizontal

    public bool controlsEnabled = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!controlsEnabled) return;

        HandleMouseLook();
        HandleMovement();
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

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D input

        // Move left/right in world space, not relative to the camera
        Vector3 move = Vector3.right * moveX;

        // Move the player body
        playerBody.position += move * moveSpeed * Time.deltaTime;

        // Slight yaw (Y-axis) rotation for lean
        float yawOffset = moveX * 10f;
        Quaternion targetRotation = Quaternion.Euler(0f, yawOffset, 0f);

        // Smooth lean left/right
        playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void disableControls()
    {
        controlsEnabled = false;
    }

}
