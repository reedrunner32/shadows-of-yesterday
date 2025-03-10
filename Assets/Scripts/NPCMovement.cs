using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private bool hasStarted = false;
    private bool wasLooking = false;
    private bool hasAppeared = false; // NPC only appears after note interaction

    public delegate void OnPlayerStartLooking();
    public static event OnPlayerStartLooking PlayerStartLooking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true; 
        gameObject.SetActive(false); // Hide NPC until the note is clicked
    }

    void Update()
    {
        if (!hasAppeared) return; // NPC does nothing until it has appeared

        Transform cam = Camera.main?.transform;

        if (cam == null)
        {
            Debug.LogError("Main Camera not found! Ensure your scene has a camera tagged as 'MainCamera'.");
            return;
        }

        bool isLooking = IsCameraLookingAtNPC(cam);

        if (isLooking && !wasLooking)
        {
            Debug.Log("Player is looking at the NPC.");
            PlayerStartLooking?.Invoke();
        }
        wasLooking = isLooking;

        if (isLooking)
        {
            if (!hasStarted)
            {
                hasStarted = true;
                agent.SetDestination(waypoints[currentWaypointIndex].position);
            }

            agent.isStopped = false;

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex < waypoints.Length)
                {
                    agent.SetDestination(waypoints[currentWaypointIndex].position);
                }
                else
                {
                    Disappear();
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    bool IsCameraLookingAtNPC(Transform cam)
    {
        Vector3 directionToNPC = (transform.position - cam.position).normalized;
        float dotProduct = Vector3.Dot(cam.forward, directionToNPC);
        return dotProduct > 0.7f;
    }

    void Disappear()
    {
        Debug.Log("NPC reached the last waypoint and disappeared.");
        gameObject.SetActive(false);
    }

    public void Appear()
    {
        Debug.Log("👀 NPC Appear() called! Attempting to activate NPC...");

        gameObject.SetActive(true);
        hasAppeared = true;

        Debug.Log("NPC Active State: " + gameObject.activeSelf);

        // Find SkinnedMeshRenderer inside child objects (Mixamo models)
        SkinnedMeshRenderer npcRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);

        if (npcRenderer != null)
        {
            npcRenderer.enabled = true; // Ensure visibility
            Debug.Log("🎭 NPC Renderer Found and Enabled!");
        }
        else
        {
            Debug.LogError("❌ ERROR: SkinnedMeshRenderer is still missing! Check child objects.");
        }
    }
}