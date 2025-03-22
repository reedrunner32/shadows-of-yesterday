using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{

    [Header("Wobble Settings")]
    public float wobbleIntensity = 0.1f; // How strong the wobble is
    public float wobbleSpeed = 1.0f;    // How fast the wobble occurs
    public bool enablePositionWobble = true; // Wobble the camera's position
    public bool enableRotationWobble = true; // Wobble the camera's rotation

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Store the camera's original position and rotation
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        // Calculate wobble offsets using sine and cosine waves
        float wobbleX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleIntensity;
        float wobbleY = Mathf.Cos(Time.time * wobbleSpeed) * wobbleIntensity;

        // Apply position wobble
        if (enablePositionWobble)
        {
            transform.localPosition = originalPosition + new Vector3(wobbleX, wobbleY, 0);
        }

        // Apply rotation wobble
        if (enableRotationWobble)
        {
            transform.localRotation = originalRotation * Quaternion.Euler(wobbleY * 10, wobbleX * 10, wobbleX * 10);
        }
    }
}
