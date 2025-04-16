using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float turnSpeed = 100f;     // How fast the wheel turns with input
    public float maxTurnAngle = 90f;   // Max rotation left/right
    public float returnSpeed = 50f;    // How fast the wheel centers itself

    private float currentAngle = 0f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); // A/D or Left/Right

        if (Mathf.Abs(input) > 0.01f)
        {
            // Steer left or right
            currentAngle += input * turnSpeed * Time.deltaTime;
        }
        else
        {
            // Slowly return to center
            currentAngle = Mathf.MoveTowards(currentAngle, 0f, returnSpeed * Time.deltaTime);
        }

        // Clamp angle and apply rotation (Z-axis for a typical steering wheel)
        currentAngle = Mathf.Clamp(currentAngle, -maxTurnAngle, maxTurnAngle);
        transform.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }
}
