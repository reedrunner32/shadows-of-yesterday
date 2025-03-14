using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public float displayTime = 3f; // Time each subtitle stays on screen
    public MonoBehaviour playerMovementScript; // Reference to movement script
    public MonoBehaviour cameraLookScript; // Reference to camera look script

    private string[] subtitles = {
        "Where am I...?",
        "This place feels familiar..",
        "I should look around..."
    };

    void Start()
    {
        // Disable player movement
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Disable camera movement
        if (cameraLookScript != null)
            cameraLookScript.enabled = false;

        StartCoroutine(ShowSubtitles());
    }

    IEnumerator ShowSubtitles()
    {
        foreach (string subtitle in subtitles)
        {
            subtitleText.text = subtitle;
            yield return new WaitForSeconds(displayTime);
        }

        subtitleText.text = ""; // Clear subtitle after last one

        // Re-enable player movement
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        // Re-enable camera movement
        if (cameraLookScript != null)
            cameraLookScript.enabled = true;
    }
}