using UnityEngine;

public class DisappearOnLookAway : MonoBehaviour
{
    public float maxDistance = 10f;
    public float fadeDuration = 1f; // Time to fade out
    private bool hasBeenSeen = false;
    private float fadeTimer = 0f;
    private Material objectMaterial;

    void Start()
    {
        // Get the object's material
        objectMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                hasBeenSeen = true;
            }
        }

        if (hasBeenSeen && !IsObjectInView())
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (fadeTimer / fadeDuration));
            Color color = objectMaterial.color;
            color.a = alpha;
            objectMaterial.color = color;

            if (fadeTimer >= fadeDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private bool IsObjectInView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}