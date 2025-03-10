using UnityEngine;

public class DisappearOnLookAway : MonoBehaviour
{
    public float maxDistance = 10f; // Maximum distance to detect the object
    private bool hasBeenSeen = false; // Track if the object has been seen

    void Update()
    {
        // Create a ray from the camera's position forward
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if the ray hits the object
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                // The player is looking at the object
                hasBeenSeen = true;
            }
        }

        // If the object has been seen and the player is no longer looking at it, disable it
        if (hasBeenSeen && !IsObjectInView())
        {
            gameObject.SetActive(false); // Disable the object permanently
        }
    }

    // Check if the object is within the camera's view
    private bool IsObjectInView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}