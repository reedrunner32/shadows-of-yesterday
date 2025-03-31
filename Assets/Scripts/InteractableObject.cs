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

        // Try to cache ShowSubtitleSequence if the manager has it
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
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
        }

        Debug.Log("Picked up " + gameObject.name);
        gameManager?.ObjectCollected();

        // ✅ Trigger that object's custom subtitle manager
        if (subtitleLines != null && subtitleLines.Length > 0 && subtitleTrigger != null)
        {
            subtitleTrigger.Invoke(subtitleLines);
        }

        OnInteract?.Invoke(gameObject);
        gameObject.SetActive(false);
    }

    public void OnHover(bool isLooking)
    {
        if (objMaterial != null)
        {
            objMaterial.SetColor("_EmissionColor", isLooking ? Color.yellow * 0.3f : Color.black);
        }
    }
}