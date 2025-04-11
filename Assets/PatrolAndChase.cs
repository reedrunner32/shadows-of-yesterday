using UnityEngine;
using UnityEngine.AI;

public class PatrolAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public float chaseSpeed = 5f;
    public float patrolSpeed = 2f;
    public float loseSightTime = 3f;
    public float proximityRadius = 2.5f;

    private NavMeshAgent agent;
    private int currentPoint = 0;
    private float timeSinceSeenPlayer = 0f;
    private enum State { Patrolling, Chasing, Returning }
    private State currentState = State.Patrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        if (patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[currentPoint].position);

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                if (CanSeePlayer())
                    StartChasing();
                break;

            case State.Chasing:
                if (player != null)
                    agent.SetDestination(player.position);
                if (!CanSeePlayer())
                {
                    timeSinceSeenPlayer += Time.deltaTime;
                    if (timeSinceSeenPlayer >= loseSightTime)
                        ReturnToPatrol();
                }
                else
                {
                    timeSinceSeenPlayer = 0f;
                }
                break;

            case State.Returning:
                if (!agent.pathPending && agent.remainingDistance < 0.2f)
                    GoToNextPoint();
                if (CanSeePlayer())
                    StartChasing();
                break;
        }

        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            transform.forward = agent.velocity.normalized;
        }
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.2f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPoint].position);
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
        currentState = State.Patrolling;
    }

    void StartChasing()
    {
        Debug.Log("🔥 Started chasing!");
        agent.speed = chaseSpeed;
        currentState = State.Chasing;
    }

    void ReturnToPatrol()
    {
        currentState = State.Returning;
        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

bool CanSeePlayer()
{
    Vector3 rayOrigin = transform.position + Vector3.up * 1.5f;
    Vector3 dirToPlayer = player.position - rayOrigin;
    float distanceToPlayer = dirToPlayer.magnitude;
    float angle = Vector3.Angle(transform.forward, dirToPlayer);

    // 🧪 Debug: show direction
    Debug.DrawRay(rayOrigin, dirToPlayer.normalized * visionRange, Color.red);

    // ✅ Immediate chase if within proximity radius
    if (distanceToPlayer <= proximityRadius)
    {
        Debug.Log("🚨 Player too close! Chasing instantly.");
        return true;
    }

    // ✅ Standard vision check (range + angle)
    if (distanceToPlayer > visionRange)
    {
        Debug.Log("❌ Outside range");
        return false;
    }

    if (angle > visionAngle / 2f)
    {
        Debug.Log("❌ Outside vision angle");
        return false;
    }

    // ✅ Raycast to check line of sight
    int layerMask = LayerMask.GetMask("PlayerVision", "Default");

    if (Physics.Raycast(rayOrigin, dirToPlayer.normalized, out RaycastHit hit, visionRange, layerMask))
    {
        Debug.Log("👀 Ray hit: " + hit.collider.name);
        return hit.collider.transform.root.CompareTag("Player");
    }

    return false;
}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You were found!");
            // You could reload the scene, stop the game, trigger jumpscare, etc.
        }
    }
}