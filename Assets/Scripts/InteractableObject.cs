using System;
using UnityEngine;

public class PickableObject : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Color originalColor;
    private Material objMaterial;
    private GameManager gameManager;

    public AudioClip pickupSound;
    private AudioSource audioSource;
    public float volume = 0.5f;

    [TextArea(2, 5)]
    public string[] subtitleLines;

    // 👇 Assign the correct manager per object in Inspector (can be any MonoBehaviour that has ShowSubtitleSequence)
    public MonoBehaviour subtitleManager; // Reference to any custom manager
    private Action<string[]> subtitleTrigger;

    public static event Action<GameObject> OnInteract;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            originalColor = objMaterial.color;

            objMaterial.EnableKeyword("_EMISSION");
            objMaterial.SetColor("_EmissionColor", Color.black);
        }

        if (subtitleManager != null)
        {
            var method = subtitleManager.GetType().GetMethod("ShowSubtitleSequence");
            if (method != null)
            {
                subtitleTrigger = (lines) => method.Invoke(subtitleManager, new object[] { lines });
            }
        }

        // Setup AudioSource for 2D sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    public void Interact()
    {
        if (pickupSound != null)
        {
            Play2DSound(pickupSound, volume);
        }

        Debug.Log("Picked up " + gameObject.name);
        gameManager?.ObjectCollected();

        if (subtitleLines != null && subtitleLines.Length > 0)
        {
            SubtitleManagerInteractives.Instance.ShowSubtitleSequence(subtitleLines);
        }

        OnInteract?.Invoke(gameObject);
        gameObject.SetActive(false);
    }

    private void Play2DSound(AudioClip clip, float vol)
    {
        GameObject tempGO = new GameObject("Temp2DAudio");
        AudioSource tempSource = tempGO.AddComponent<AudioSource>();

        tempSource.clip = clip;
        tempSource.volume = vol;
        tempSource.spatialBlend = 0f; // 👈 Fully 2D
        tempSource.Play();

        // Destroy after the clip finishes
        Destroy(tempGO, clip.length);
    }

    public void OnHover(bool isLooking)
    {
        if (objMaterial != null)
        {
            objMaterial.SetColor("_EmissionColor", isLooking ? Color.yellow * 0.3f : Color.black);
        }
    }
}