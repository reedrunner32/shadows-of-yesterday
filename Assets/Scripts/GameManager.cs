using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int collectedItems = 0;
    private int totalItems = 4; // Change if we decide to add more items

    public void ObjectCollected()
    {
        collectedItems++;
        
        if (collectedItems >= totalItems)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("ToBeContinued"); // Change this in the future when we arent in beta test or if we have car scene ready
    }
}
