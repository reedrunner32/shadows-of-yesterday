using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public GameObject player;
    public bool canInteract = false;
    private bool isPlayerNearby = false;

    void Update()
    {
        if (canInteract && isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Do whatever opening means (e.g. animation, disable collider, etc)
            Debug.Log("Door opened!");
            SceneManager.LoadScene("MorgueScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            isPlayerNearby = false;
    }
}
