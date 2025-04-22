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

    // Reference to the door script
    public SojaExiles.opencloseDoor doorScript;
    public GameObject doorCollider;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            objMaterial = objRenderer.material;
            originalColor = objMaterial.color;

            objMaterial.EnableKeyword("_EMISSION");
            objMaterial.SetColor("_EmissionColor", originalColor); // No glow initially
        }

        // Setup AudioSource for 2D sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }

    public void Interact()
    {
        Debug.Log("📝 NoteInteract.Interact() called on: " + gameObject.name);

        // Play pickup sound
        if (pickupSound != null)
        {
            Play2DSound(pickupSound, volume);
        }

        // If this object is linked to an NPC, make the NPC appear
        if (npc != null)
        {
            Debug.Log("✅ NPC reference found! Triggering NPC appearance.");
            npc.Appear();
        }

        // Open the door if referenced
        if (doorScript != null)
        {
            Debug.Log("🚪 Door reference found! Toggling door.");
            doorScript.ToggleDoor();
        }
        else
        {
            Debug.LogError("❌ ERROR: No door script reference found!");
        }

        if (doorCollider != null)
        {
            doorCollider.SetActive(true);
        }

        // Remove glow effect
        if (objMaterial != null)
        {
            objMaterial.SetColor("_EmissionColor", originalColor);
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
        SubtitleManagerInteractives.Instance.ShowSubtitleSequence(new string[] {
            "Divorce?",
            "Since when are we even at this point?",
            "Did I miss something… or has something happened…"
        });
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
            objMaterial.SetColor("_EmissionColor", isLooking ? Color.yellow * 0.3f : originalColor);
        }
    }
}