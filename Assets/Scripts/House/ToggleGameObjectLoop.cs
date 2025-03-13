using System.Collections;
using UnityEngine;

public class ToggleGameObjectLoop : MonoBehaviour
{
    public GameObject targetObject; // The object to toggle
    public float minDelay = 0.1f; // Minimum delay in seconds
    public float maxDelay = 0.5f; // Maximum delay in seconds
    public float longDelay = 1.5f;

    private void Awake()
    {
        if (targetObject != null)
        {
            StartCoroutine(FlickerEffect());
        }
    }

    private IEnumerator FlickerEffect()
    {
        while (true)
        {
            float randomDelay = Random.Range(minDelay, maxDelay);
            if (randomDelay > maxDelay - minDelay)
                randomDelay = longDelay;
            yield return new WaitForSeconds(randomDelay);
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
