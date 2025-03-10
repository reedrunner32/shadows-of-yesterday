using System.Collections;
using UnityEngine;

public class NoteInteract : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Color originalColor;
    private Material objMaterial;
    private GameManager gameManager;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public float volume = 0.5f;

    // NPC reference (only needed for the note)
    public NPCMovement npc;

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

        // Ensure AudioSource is available
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Interact()
    {
        Debug.Log("📝 NoteInteract.Interact() called on: " + gameObject.name);

        // Play pickup sound
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
        }

        // If this object is linked to an NPC, make the NPC appear
        if (npc != null)
        {
            Debug.Log("✅ NPC reference found! Triggering NPC appearance.");
            npc.Appear();
        }

        // Remove glow effect
        if (objMaterial != null)
        {
            objMaterial.SetColor("_EmissionColor", Color.black);
        }

        // Now disable the object
        gameObject.SetActive(false);

        // Track object collection in the GameManager
        if (gameManager != null)
        {
            gameManager.ObjectCollected();
        }
        else
        {
            Debug.LogError("❌ ERROR: GameManager reference is missing!");
        }
    }

    public void OnHover(bool isLooking)
    {
        if (objMaterial != null)
        {
            objMaterial.SetColor("_EmissionColor", isLooking ? Color.yellow * 0.3f : Color.black);
        }
    }
}