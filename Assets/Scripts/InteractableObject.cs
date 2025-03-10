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

    // Define a static event for interaction
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
            objMaterial.SetColor("_EmissionColor", Color.black); // No glow initially
        }
    }

    public void Interact()
    {
        // Play the pickup sound
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
        }

        Debug.Log("Picked up " + gameObject.name);
        gameManager.ObjectCollected(); // Keeps track of how many objects we have picked up
        
        // Trigger the Interact event
        OnInteract?.Invoke(gameObject);
        gameObject.SetActive(false);
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