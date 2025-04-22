using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class InteractableRadio : MonoBehaviour, IInteractable
{
    private Renderer objRenderer;
    private Material objMaterial;
    private GameManager gameManager;
    private AudioSource audioSource;
    public float fadeDuration = 2f;
    public RawImage fadeImage;

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

    private IEnumerator LoadAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(28 - fadeDuration);
        float startVolume = audioSource.volume;

        Color color = fadeImage.color;
        color = Color.black;
        color.a = 0f;
        fadeImage.color = color;

        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
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

        StartCoroutine(LoadAfterDelay("CarScene"));
        StartCoroutine(FadeOutCoroutine());
    }

    public void OnHover(bool isLooking)
    {
        if (hasInteracted || objMaterial == null) return;

        objMaterial.SetColor("_EmissionColor", isLooking ? Color.yellow * 0.3f : Color.black);
    }
}