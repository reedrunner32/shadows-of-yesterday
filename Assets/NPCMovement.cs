using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private bool hasStarted = false;
    private bool wasLooking = false; // Track if player was previously looking

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true; // Start with movement disabled
    }

    void Update()
    {
        Transform cam = Camera.main?.transform; // Ensure there's a main camera

        if (cam == null)
        {
            Debug.LogError("Main Camera not found! Ensure your scene has a camera tagged as 'MainCamera'.");
            return;
        }

        bool isLooking = IsCameraLookingAtNPC(cam);

        // Debug log when the player starts looking
        if (isLooking && !wasLooking)
        {
            Debug.Log("Player is looking at the NPC.");
        }
        wasLooking = isLooking; // Update tracking

        if (isLooking)
        {
            if (!hasStarted)
            {
                hasStarted = true; // Start movement on first look
                agent.SetDestination(waypoints[currentWaypointIndex].position);
            }

            agent.isStopped = false; // Resume movement

            // Move to next waypoint if close enough
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex < waypoints.Length)
                {
                    agent.SetDestination(waypoints[currentWaypointIndex].position);
                }
                else
                {
                    Disappear(); // NPC disappears at the last waypoint
                }
            }
        }
        else
        {
            agent.isStopped = true; // Pause movement when not looked at
        }
    }

    bool IsCameraLookingAtNPC(Transform cam)
    {
        Vector3 directionToNPC = (transform.position - cam.position).normalized;
        Vector3 cameraForward = cam.forward;

        float dotProduct = Vector3.Dot(cameraForward, directionToNPC);

        return dotProduct > 0.7f; // Adjust threshold for sensitivity
    }

    void Disappear()
    {
        Debug.Log("NPC reached the last waypoint and disappeared.");
        gameObject.SetActive(false); // Hide NPC (Use Destroy(gameObject) to remove it)
    }
}