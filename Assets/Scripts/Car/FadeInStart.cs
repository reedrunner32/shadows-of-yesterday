using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInStart : MonoBehaviour
{
    public Image fadeImage;
    public float duration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        Color color = fadeImage.color;
        color.a = 255f;
        fadeImage.color = color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / duration);
            fadeImage.color = color;
            yield return null;
        }

    }
}
