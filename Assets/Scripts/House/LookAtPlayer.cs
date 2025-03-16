using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform

    void Update()
    {
        // Make this object look at the player
        transform.LookAt(player);
    }
}
