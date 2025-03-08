using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int collectedItems = 0;
    private int totalItems = 4; // Change if we decide to add more items

    private void Start()
    {
        UpdateUI();
    }

    public TextMeshProUGUI collectedText;

    public void ObjectCollected()
    {
        collectedItems++;
        UpdateUI();
        
        if (collectedItems >= totalItems)
        {
            LoadNextScene();
        }
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
