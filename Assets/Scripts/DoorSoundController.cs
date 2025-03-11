using UnityEngine;

public class DoorSoundController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip doorCloseSound; // Sound to play when the door closes

    public void PlayDoorCloseSound()
    {
        if (audioSource != null && doorCloseSound != null)
        {
            audioSource.PlayOneShot(doorCloseSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or doorCloseSound is not assigned.");
        }
    }
}