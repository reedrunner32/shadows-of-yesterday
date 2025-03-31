using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManagerNote : MonoBehaviour
{
    public TMP_Text subtitleText;
    public float displayTime = 3f;

    private Coroutine currentSequence;

    public void ShowSubtitleSequence(string[] lines)
    {
        // Stop any active subtitle sequences
        if (currentSequence != null)
        {
            StopCoroutine(currentSequence);
        }

        currentSequence = StartCoroutine(PlaySubtitles(lines));
    }

    private IEnumerator PlaySubtitles(string[] lines)
    {
        subtitleText.gameObject.SetActive(true);

        foreach (string line in lines)
        {
            subtitleText.text = line;
            yield return new WaitForSeconds(displayTime);
        }

        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
        currentSequence = null;
    }
}