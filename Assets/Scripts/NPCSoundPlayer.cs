using UnityEngine;

public class NPCSoundPlayer : MonoBehaviour
{
    public AudioClip lookSound; // Sound to play when the player looks at the NPC
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource for 3D sound
        audioSource.spatialBlend = 1.0f; // Set to 3D sound
        audioSource.minDistance = 1.0f; // Minimum distance for full volume
        audioSource.maxDistance = 10.0f; // Maximum distance for hearing the sound
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; // Adjust rolloff mode

        // Subscribe to the PlayerStartLooking event
        NPCMovement.PlayerStartLooking += PlayLookSound;
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        NPCMovement.PlayerStartLooking -= PlayLookSound;
    }

    void PlayLookSound()
    {
        if (lookSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(lookSound); // Play the sound through the AudioSource
        }
        else
        {
            Debug.LogWarning("Look sound or AudioSource is missing!");
        }
    }
}