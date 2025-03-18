using UnityEngine;

public class DisappearWhenClose : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float disappearDistance = 3f; // Distance at which the object disappears

    void Update()
    {
        // Check the distance between the object and the player
        if (Vector3.Distance(transform.position, player.position) < disappearDistance)
        {
            // Disable the object
            gameObject.SetActive(false);
        }
    }
}
