using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        // This only works in a built application, not in the editor
        Application.Quit();

        // For debugging in the editor
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
