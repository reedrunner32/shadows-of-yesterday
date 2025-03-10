using System.Collections;
using UnityEngine;

public class DoorAutoClose : MonoBehaviour
{
    public SojaExiles.opencloseDoor doorScript;
    public float delayBeforeClosing = 2f; // Delay in seconds

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🚪 Player entered the bathroom. Closing door after delay...");
            if (doorScript != null && doorScript.open)
            {
                StartCoroutine(CloseDoorAfterDelay());
            }
        }
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeClosing);
        if (doorScript.open) // Ensure it's still open before closing
        {
            doorScript.ToggleDoor();
        }
    }
}