using UnityEngine;
using TMPro;
using System.Collections;

public class carSubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI carSubtitleText;
    public float initialDelay = 1.5f; // Time before first subtitle appears
    public float baseDisplayTime = 3f; // Base time for short subtitles
    public float additionalTimePerCharacter = 0.05f; // Additional time per character
    public float minimumDisplayTime = 2f; // Minimum time for any subtitle
    public float delayBetweenSubtitles = 0.5f; // Brief pause between lines
    public MonoBehaviour playerCarMovementScript; // Reference to movement script
    public MonoBehaviour cameraCarLookScript; // Reference to camera look script

    private string[] carSubtitles = {
        "Daniel, you can't keep doing this.",
        "You're never home.",
        "And when you are, you're distracted, like you're not really here...",
        "I'm trying.",
        "Do you have any idea how much pressure I'm under at work?",
        "That's not an excuse! Lily needs you. I need you.",
        "I'm doing the best I can!",
        "You think I don't know what's at stake? You think I don't care?",
        "Do you? Because it doesn't feel like it. Lily asked for you at her recital last week. You said you'd be there. But, surprise, you weren't... again.",
        "I had a meeting I couldn't miss. You know how it is. If I don't keep up, we lose everything. Is that what you want?",
        "What I want? What I want is for you to realize that 'everything' isn't just your job. It's us. Your family. Or have you forgotten that?",
        "What good is it if you're never here to live it with us? Lily barely knows you anymore. She's starting to think you don't care about her.",
    };

    void Start()
    {
        // Disable player movement
        if (playerCarMovementScript != null)
            playerCarMovementScript.enabled = false;

        // Disable camera movement
        if (cameraCarLookScript != null)
            cameraCarLookScript.enabled = false;

        StartCoroutine(ShowCarSubtitles());
    }

    IEnumerator ShowCarSubtitles()
    {
        // Initial delay before first subtitle
        yield return new WaitForSeconds(initialDelay);

        foreach (string carSubtitle in carSubtitles)
        {
            carSubtitleText.text = carSubtitle;

            // Calculate display time based on text length
            float calculatedTime = Mathf.Max(
                minimumDisplayTime,
                baseDisplayTime + (carSubtitle.Length * additionalTimePerCharacter)
            );

            yield return new WaitForSeconds(calculatedTime);

            // Brief pause between subtitles
            carSubtitleText.text = "";
            yield return new WaitForSeconds(delayBetweenSubtitles);
        }

        // Clear final subtitle
        carSubtitleText.text = "";

        // Re-enable player movement
        if (playerCarMovementScript != null)
            playerCarMovementScript.enabled = true;

        // Re-enable camera movement
        if (cameraCarLookScript != null)
            cameraCarLookScript.enabled = true;
    }
}