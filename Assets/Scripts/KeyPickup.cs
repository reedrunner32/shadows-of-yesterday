using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject player;
    public GameObject[] lightsToTurnOff;
    public GameObject door;
    public Light doorLight;
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;
    public GameObject npc4;
    public GameObject npc5;

    private bool isPlayerNearby = false;

    void Update()
    {
        doorLight.enabled = false;
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key picked up!");

            npc1.SetActive(false);
            npc2.SetActive(false);
            npc3.SetActive(false);
            npc4.SetActive(false);
            npc5.SetActive(false);

            // Turn off lights (get Light components from GameObjects)
            foreach (GameObject lightObj in lightsToTurnOff)
            {
                Light lightComp = lightObj.GetComponent<Light>();
                if (lightComp != null)
                {
                    lightComp.enabled = false;
                    Debug.Log("Turned off light: " + lightObj.name);
                }
                else
                {
                    Debug.LogWarning("No Light component found on: " + lightObj.name);
                }
            }

            // Enable door interaction
            DoorInteraction doorScript = door.GetComponent<DoorInteraction>();
            if (doorScript != null)
            {
                doorScript.canInteract = true;
                Debug.Log("Door is now interactable.");
            }

            // Turn on door light
            if (doorLight != null)
            {
                doorLight.enabled = true;
                Debug.Log("Door light turned on.");
            }
            else
            {
                Debug.LogWarning("Door light reference is missing!");
            }

            // Destroy key object
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerNearby = true;
            Debug.Log("Player entered key area.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerNearby = false;
            Debug.Log("Player left key area.");
        }
    }
}
