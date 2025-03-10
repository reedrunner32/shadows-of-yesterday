using System.Collections;
using UnityEngine;

public class PickableObjectRadio : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Color originalColor;
    private Material objMaterial;
    private GameManager gameManager;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public float volume = 0.5f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            originalColor = objMaterial.color;

            objMaterial.EnableKeyword("_EMISSION");
            objMaterial.SetColor("_EmissionColor", Color.black); // No glow initially
        }

        // Ensure an AudioSource is available
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Interact()
    {
        if (pickupSound != null)
        {
            audioSource.clip = pickupSound;
            audioSource.volume = volume;
            audioSource.Play();

            Debug.Log("▶️ Playing radio sound...");
        }
        else
        {
            Debug.LogError("❌ ERROR: No pickup sound assigned to " + gameObject.name);
        }

        // Remove glow effect but KEEP the radio visible
        if (objMaterial != null)
        {
            objMaterial.SetColor("_EmissionColor", Color.black); // Remove glow
        }

        // Notify GameManager if needed
        if (gameManager != null)
        {
            gameManager.ObjectCollected();
        }
    }

    public void OnHover(bool isLooking)
    {
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