using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManagerInteractives : MonoBehaviour
{
    public static SubtitleManagerInteractives Instance;

    public TMP_Text subtitleText;
    public float displayTime = 3f;

    private Coroutine currentSequence;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ShowSubtitleSequence(string[] lines, float customDisplayTime = -1f)
    {
        if (currentSequence != null)
            StopCoroutine(currentSequence);

        currentSequence = StartCoroutine(PlaySubtitles(lines, customDisplayTime > 0 ? customDisplayTime : displayTime));
    }

    private IEnumerator PlaySubtitles(string[] lines, float delay)
    {
        subtitleText.gameObject.SetActive(true);

        foreach (string line in lines)
        {
            subtitleText.text = line;
            yield return new WaitForSeconds(delay);
        }

        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
        currentSequence = null;
    }
}