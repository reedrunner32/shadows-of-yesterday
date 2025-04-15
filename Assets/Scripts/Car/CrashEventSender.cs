using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CrashEventSender : MonoBehaviour
{
    public PlayerCar script;
    public Image whiteFadeImage;

    public float duration = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.startCrashEvent();
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        float timer = 0f;

        Color color = whiteFadeImage.color;
        color = Color.white;
        color.a = 0f;
        whiteFadeImage.color = color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / duration);
            whiteFadeImage.color = color;
            yield return null;
        }
    }

}
