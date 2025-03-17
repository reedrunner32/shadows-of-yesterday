using System;
using UnityEngine;

public class InteractableRadio : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Material objMaterial;
    private GameManager gameManager;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public float volume = 0.5f;

    // Define a static event for interaction
    public static event Action<GameObject> OnInteract;

    // Flag to track if the interaction has already occurred
    private bool hasInteracted = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;

            objMaterial.EnableKeyword("_EMISSION");
            objMaterial.SetColor("_EmissionColor", Color.black); // No glow initially
        }
    }

    public void Interact()
    {

        if (hasInteracted) return;

        // Play the pickup sound
        if (pickupSound != null)
        {
            audioSource.clip = pickupSound;
            audioSource.loop = false;
            audioSource.volume = Mathf.Clamp01(volume);
            audioSource.Play();
        }

        gameManager.ObjectCollected(); // Keeps track of how many objects we have picked up

        // Trigger the Interact event
        OnInteract?.Invoke(gameObject);
        hasInteracted = true;
        objMaterial.SetColor("_EmissionColor", Color.black); // remove glow
    }

    public void OnHover(bool isLooking)
    {
        if (hasInteracted) return;

        if (objMaterial != null)
        {
            if (isLooking)
            {
                objMaterial.SetColor("_EmissionColor", Color.yellow * 0.3f); // Subtle yellow glow
            }
            else
            {
                objMaterial.SetColor("_EmissionColor", Color.black); // Remove glow
            }
        }
    }
}