using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject radio;
    public GameObject diningLight;
    public string hexColor = "#FF5733";

    private int collectedItems = 0;
    private int totalItems = 4; // Change if we decide to add more items
    private bool lastItem = false;

    private void Start()
    {
        UpdateUI();
    }

    public TextMeshProUGUI collectedText;

    public void ObjectCollected()
    {
        collectedItems++;
        UpdateUI();

        if(!lastItem && totalItems - collectedItems == 1)
        {
            Light lightComponent = diningLight.GetComponent<Light>();

            if (lightComponent != null)
            {
                if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
                {
                    lightComponent.color = color;
                }
                else
                {
                    Debug.LogWarning("Invalid hex color format.");
                }
            }
            else
            {
                Debug.LogWarning("No Light component found on this GameObject.");
            }

            if (radio != null)
            {
                radio.SetActive(true);
            }
            lastItem = true;
        }
        
        //if (collectedItems >= totalItems)
        //{
        //    Invoke("LoadNextScene", 28f);
        //}
    }

    private void UpdateUI()
    {
        collectedText.text = "" + collectedItems + " / " + totalItems;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("ToBeContinued"); // Change this in the future when we arent in beta test or if we have car scene ready
    }
}
