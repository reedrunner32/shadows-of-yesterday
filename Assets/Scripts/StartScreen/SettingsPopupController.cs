using UnityEngine;

public class SettingsPopupController : MonoBehaviour
{
    public GameObject settingsPopup;

    public void OpenSettings()
    {
        settingsPopup.SetActive(true);
        // Optionally pause the game
        //Time.timeScale = 0f;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void CloseSettings()
    {
        settingsPopup.SetActive(false);
        // Resume the game
        //Time.timeScale = 1f;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPopup.activeSelf)
                CloseSettings();
            else
                OpenSettings();
        }
    }

}
