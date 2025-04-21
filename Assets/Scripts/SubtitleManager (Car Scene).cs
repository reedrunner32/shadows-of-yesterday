using UnityEngine;
using TMPro;
using System.Collections;

public class carSubtitleManager : MonoBehaviour
{
    [Header("Subtitle Settings")]
    public TextMeshProUGUI carSubtitleText;
    public float initialDelay = 0.7f;
    public float baseDisplayTime = 0.8f;           // Reduced from 3f for quicker short lines
    public float additionalTimePerCharacter = 0.02f; // Reduced from 0.05f for less length scaling
    public float minimumDisplayTime = 0.5f;           // Reduced from 2f for very short lines
    public float delayBetweenSubtitles = 0.2f;

    [Header("Player Control References")]
    public MonoBehaviour playerCarMovementScript;
    public MonoBehaviour cameraCarLookScript;

    private string[] carSubtitles = {
        "Emily: Daniel, you can't keep doing this.",
        "You're never home.",
        "And when you are, you're distracted, like you're not really here...",
        "Daniel: I'm trying.",
        "Do you have any idea how much pressure I'm under at work?",
        "Emily: That's not an excuse! Lily needs you. I need you.",
        "Daniel: I'm doing the best I can!",
        "You think I don't know what's at stake? You think I don't care?",
        "Emily: Do you? Because it doesn't feel like it. Lily asked for you at her recital last week. You said you'd be there. But, surprise, you weren't... again.",
        "Daniel: I had a meeting I couldn't miss. You know how it is. If I don't keep up, we lose everything. Is that what you want?",
        "Emily: What I want...? What I want is for you to realize that 'everything' isn't just your job. It's us. Your family. Or have you forgotten that?",
        "What good is it if you're never here to live it with us? Lily barely knows you anymore. She's starting to think you don't care about her.",
    };

    void Start()
    {
        // Debug warnings for missing references
        if (carSubtitleText == null)
            Debug.LogError("carSubtitleText is not assigned in the Inspector!", this);

        if (playerCarMovementScript == null)
            Debug.LogWarning("playerCarMovementScript reference is missing.", this);

        if (cameraCarLookScript == null)
            Debug.LogWarning("cameraCarLookScript reference is missing.", this);

        // Disable controls if references exist
        SetPlayerControls(false);
        StartCoroutine(ShowCarSubtitles());
    }

    IEnumerator ShowCarSubtitles()
    {
        // Exit early if text object is missing
        if (carSubtitleText == null)
        {
            Debug.LogError("Cannot show subtitles: carSubtitleText is null!", this);
            SetPlayerControls(true);
            yield break;
        }

        yield return new WaitForSeconds(initialDelay);

        foreach (string subtitle in carSubtitles)
        {
            carSubtitleText.text = subtitle;

            // Calculate dynamic display time (shorter lines disappear faster)
            float displayTime = Mathf.Max(
                minimumDisplayTime,
                baseDisplayTime + (subtitle.Length * additionalTimePerCharacter)
            );

            // Optional: Debug the calculated time for testing
            Debug.Log($"Showing: '{subtitle}' for {displayTime:F1}s");

            yield return new WaitForSeconds(displayTime);

            // Clear text and pause between lines
            carSubtitleText.text = "";
            yield return new WaitForSeconds(delayBetweenSubtitles);
        }

        // Cleanup
        carSubtitleText.text = "";
        SetPlayerControls(true);
    }

    private void SetPlayerControls(bool enabled)
    {
        if (playerCarMovementScript != null)
            playerCarMovementScript.enabled = enabled;

        if (cameraCarLookScript != null)
            cameraCarLookScript.enabled = enabled;
    }
}