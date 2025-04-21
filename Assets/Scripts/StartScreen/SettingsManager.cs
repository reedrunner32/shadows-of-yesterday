using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public float mouseSensitivity = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSensitivity(float value)
    {
        mouseSensitivity = value;
    }

    public float GetSensitivity()
    {
        return mouseSensitivity;
    }
}
