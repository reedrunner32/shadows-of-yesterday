using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    // Set this to the position you want the player to teleport to
    public Vector3 teleportDestination = new Vector3(0, 0, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = teleportDestination;
        }
    }
}
