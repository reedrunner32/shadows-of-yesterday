using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float rotationSpeed = 50f; // Degrees per second

    void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
