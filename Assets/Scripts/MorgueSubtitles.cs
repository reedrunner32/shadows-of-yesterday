using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class MorgueSubtitles : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public Light[] lightsToTurnOff;
    public float displayTime = 4f; // Time each subtitle stays on screen

    private string[] subtitles = {
        "Who are they..?",
        "No...It can't be.",
        "...My family?"
    };

    void Start()
    {
        StartCoroutine(ShowSubtitles());
    }

    IEnumerator ShowSubtitles()
    {
        foreach (string subtitle in subtitles)
        {
            subtitleText.text = subtitle;
            yield return new WaitForSeconds(displayTime);
        }

        subtitleText.text = ""; // Clear subtitle after last one

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("EndScreen");

    }
}