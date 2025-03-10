using UnityEngine;
using TMPro;
using System.Collections;

public class StuffedAnimal : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float displayTime = 3f;
    private bool hasInteracted = false;

    private string[] subtitles = {
        "Amy..?",
        "She used to love this thing..."
    };

    void Start()
    {
        subtitleText.text = ""; // Ensure text is empty at start
        PickableObject.OnInteract += HandleInteraction; // Subscribe to event
    }

    void OnDestroy()
    {
        PickableObject.OnInteract -= HandleInteraction; // Unsubscribe to prevent memory leaks
    }

    private void HandleInteraction(GameObject interactedObject)
    {
        // Check if the interacted object is THIS stuffed animal (bunny)
        if (!hasInteracted && interactedObject == gameObject)
        {
            hasInteracted = true;
            StartCoroutine(ShowSubtitles());
        }
    }

    IEnumerator ShowSubtitles()
    {
        foreach (string subtitle in subtitles)
        {
            subtitleText.text = subtitle;
            yield return new WaitForSeconds(displayTime);
        }

        subtitleText.text = ""; // Clear subtitle after last one
    }
}
