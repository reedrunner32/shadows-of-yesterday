using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    public GameObject notePanel;
    public TextMeshProUGUI noteText;
    [TextArea(5, 10)] public string noteContent;

    private bool isDisplayed = false;

    void Start()
    {
        if (notePanel != null)
        {
            notePanel.SetActive(false);
        }
    }

    public void Interact()
    {
        if (!isDisplayed)
        {
            ShowNote();
        }
        else
        {
            HideNote();
        }
    }

    private void ShowNote()
    {
        if (notePanel != null)
        {
            notePanel.SetActive(true);
            noteText.text = noteContent;
            isDisplayed = true;
        }
    }

    private void HideNote()
    {
        if (notePanel != null)
        {
            notePanel.SetActive(false);
            isDisplayed = false;
        }
    }

    void Update()
    {
        if (isDisplayed && Input.GetKeyDown(KeyCode.Escape)) // Escape key to close, but maybe button instead
        {
            HideNote();
        }
    }
}
