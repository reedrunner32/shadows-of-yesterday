using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CrashEventSender : MonoBehaviour
{
    public PlayerCar script;
    public Image whiteFadeImage;
    public AudioSource horn;

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
        horn.Play();
        float startVolume = horn.volume;

        float timer = 0f;

        Color color = whiteFadeImage.color;
        color = Color.white;
        color.a = 0f;
        whiteFadeImage.color = color;

        while (timer < duration)
        {
            horn.volume = Mathf.Lerp(startVolume, 0f, timer / duration);

            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / duration);
            whiteFadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene("HospitalScene");

    }

}
