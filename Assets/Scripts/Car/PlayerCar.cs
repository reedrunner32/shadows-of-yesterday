using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCar : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float swaySpeed = 1.5f;
    public float swayAmount = 0.5f;
    public float minTimeBetweenSways = 1f;
    public float maxTimeBetweenSways = 3f;

    public Image whiteFadeImage; // Assign in Inspector

    private Rigidbody rb;
    private float swayTimer;
    private float nextSwayTime;
    private float targetSway = 0f;
    private float currentSway = 0f;

    private float swayIncreaseTimer = 0f;
    private float swayIncreaseInterval = 1f;

    private bool hasCollided = false;
    public FirstPersonCamera firstPersonCamera;
    public AudioSource carAmbience;
    public AudioSource wifeDialogue;
    public AudioSource fatherDialogue;

    public GameObject crashTarget; // Target to move towards on crashEvent
    public bool crashEvent = false; // made public for external triggering

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ScheduleNextSway();
    }

    void Update()
    {
        if (hasCollided) return;

        swayIncreaseTimer += Time.deltaTime;
        if (swayIncreaseTimer >= swayIncreaseInterval)
        {
            swayAmount += 0.02f;
            forwardSpeed += 0.1f;
            swayIncreaseTimer = 0f;
        }
    }

    // Add at top:
    public float strafeAcceleration = 20f;
    public float strafeDeceleration = 10f;
    public float maxStrafeSpeed = 5f;
    private float currentStrafeVelocity = 0f;

    void FixedUpdate()
    {
        if (hasCollided) return;

        if (crashEvent && crashTarget != null)
        {
            Vector3 direction = (crashTarget.transform.position - transform.position).normalized;
            rb.velocity = direction * forwardSpeed;
            return;
        }

        // Sway logic (optional or additive)
        swayTimer += Time.fixedDeltaTime;
        if (swayTimer >= nextSwayTime)
        {
            float dir = Random.value < 0.5f ? -1f : 1f;
            targetSway = dir * swayAmount;

            swayTimer = 0f;
            ScheduleNextSway();
        }
        currentSway = Mathf.Lerp(currentSway, targetSway, Time.fixedDeltaTime * swaySpeed);

        // Input-based strafe
        float inputX = Input.GetAxisRaw("Horizontal");

        if (inputX != 0)
        {
            currentStrafeVelocity = Mathf.MoveTowards(currentStrafeVelocity, inputX * maxStrafeSpeed, strafeAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentStrafeVelocity = Mathf.MoveTowards(currentStrafeVelocity, 0f, strafeDeceleration * Time.fixedDeltaTime);
        }

        Vector3 forward = transform.forward * forwardSpeed;
        Vector3 lateral = transform.right * (currentSway + currentStrafeVelocity);

        // Optional: Slight yaw (Y-axis) rotation for lean
        float yawOffset = currentStrafeVelocity / maxStrafeSpeed * 10f;
        Quaternion targetRotation = Quaternion.Euler(0f, yawOffset, 0f);
        firstPersonCamera.playerBody.rotation = Quaternion.Slerp(firstPersonCamera.playerBody.rotation, targetRotation, Time.fixedDeltaTime * 5f);

        rb.velocity = forward + lateral;
    }



    void ScheduleNextSway()
    {
        nextSwayTime = Random.Range(minTimeBetweenSways, maxTimeBetweenSways);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            hasCollided = true;
            rb.velocity = Vector3.zero;

            if (whiteFadeImage != null)
            {
                firstPersonCamera.disableControls();
                if (carAmbience != null) carAmbience.Stop();
                if (wifeDialogue != null) wifeDialogue.Stop();
                if (fatherDialogue != null) fatherDialogue.Stop();
                StartCoroutine(FadeAndRestart());
            }
            else
            {
                Invoke(nameof(RestartScene), 2f);
            }
        }
    }

    IEnumerator FadeAndRestart()
    {
        float duration = 2f;
        float timer = 0f;

        Color color = whiteFadeImage.color;
        color = Color.black;
        color.a = 0f;
        whiteFadeImage.color = color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / duration);
            whiteFadeImage.color = color;
            yield return null;
        }

        RestartScene();
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void startCrashEvent()
    {
        firstPersonCamera.disableControls();
        crashEvent = true;
    }
}
