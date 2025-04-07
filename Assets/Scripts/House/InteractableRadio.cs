using System;
using UnityEngine;

public class InteractableRadio : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Material objMaterial;
    private GameManager gameManager;
    private AudioSource audioSource;

    public AudioClip pickupSound;
    public float volume = 0.5f;

    public string[] subtitleLines; // ✅ Subtitles specific to the radio
    public MonoBehaviour subtitleManager; // ✅ Drag in your SubtitleManagerRadio (must have ShowSubtitleSequence)

    public static event Action<GameObject> OnInteract;

    private bool hasInteracted = false;
    private Action<string[]> subtitleTrigger;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            objMaterial.EnableKeyword("_EMISSION");
            objMaterial.SetColor("_EmissionColor", Color.black);
        }

        // Cache the subtitle trigger if the method exists
        if (subtitleManager != null)
        {
            var method = subtitleManager.GetType().GetMethod("ShowSubtitleSequence");
            if (method != null)
            {
                subtitleTrigger = (lines) => method.Invoke(subtitleManager, new object[] { lines });
            }
        }
    }

    public void Interact()
    {
        if (hasInteracted) return;

        // Play the pickup sound
        if (pickupSound != null && audioSource != null)
        {
            audioSource.clip = pickupSound;
            audioSource.loop = false;
            audioSource.volume = Mathf.Clamp01(volume);
            audioSource.Play();
        }

        gameManager?.ObjectCollected();

        // ✅ Trigger subtitle playback if available
        if (subtitleLines != null && subtitleLines.Length > 0)
        {
            SubtitleManagerInteractives.Instance.ShowSubtitleSequence(subtitleLines);
        }

        OnInteract?.Invoke(gameObject);
        hasInteracted = true;

        if (objMaterial != null)
            objMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void OnHover(bool isLooking)
    {
        if (hasInteracted || objMaterial == null) return;

        objMaterial.SetColor("_EmissionColor", isLooking ? Color.yellow * 0.3f : Color.black);
    }
}