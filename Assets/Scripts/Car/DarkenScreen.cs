using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenScreen : MonoBehaviour
{
    public RawImage fadeImage;
    public Transform objectA;
    public Transform objectB;
    public float startDistance = 100f; // The distance at which alpha = 0
    public float endDistance = 1f;    // The distance at which alpha = 0.9

    void Start()
    {
        startDistance = Vector3.Distance(objectA.position, objectB.position);
    }

    void Update()
    {

        float currentDistance = Vector3.Distance(objectA.position, objectB.position);

        // Clamp the distance within the start and end range
        float clampedDistance = Mathf.Clamp(currentDistance, endDistance, startDistance);

        // Calculate how far we've progressed from startDistance to endDistance
        float t = 1f - ((clampedDistance - endDistance) / (startDistance - endDistance)); // Normalized ratio
        float alpha = Mathf.Lerp(0f, 0.9f, t);

        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}
