using UnityEngine;

public class RadioInteract : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    if (audioSource == null)
    {
        Debug.LogError("AudioSource is missing on " + gameObject.name);
    }
    }

    void OnMouseDown() // Detects clicks on the radio
    {
        if (audioSource != null)
        {
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
            else
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }
    }
}