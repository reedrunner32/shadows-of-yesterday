using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenScreen : MonoBehaviour
{
    public RawImage fadeImage;
    public float fadeInDuration = 10f;
    public float fadeOutDuration = 2f;

    private float timer = 0f;
    private bool fadingIn = true;
    private Color color;

    void Start()
    {
        color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (fadingIn)
        {
            float t = timer / fadeInDuration;
            color.a = Mathf.Lerp(0f, 0.7f, t);
            fadeImage.color = color;

            if (t >= 1f)
            {
                // Start fading out
                fadingIn = false;
                timer = 0f;
            }
        }
        else
        {
            float t = timer / fadeOutDuration;
            color.a = Mathf.Lerp(0.7f, 0f, t);
            fadeImage.color = color;

            if (t >= 1f)
            {
                // Reset to fade in again if needed
                fadingIn = true;
                timer = 0f;
            }
        }
    }
}
