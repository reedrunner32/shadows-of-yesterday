using UnityEngine;

public class ShowWifeShadow : MonoBehaviour
{
    public GameObject wife;
    public GameObject hallwayLight;
    public GameObject lightFlicker;

    private void OnEnable()
    {
        // Subscribe to the Interact event
        PickableObject.OnInteract += ShowWife;
    }

    private void OnDisable()
    {
        // Unsubscribe from the Interact event
        PickableObject.OnInteract -= ShowWife;
    }

    private void ShowWife(GameObject interactedObject)
    {
        // Check if the interacted object is the one this script is attached to
        if (interactedObject == gameObject) {
            if (wife != null)
            {
                wife.SetActive(true);
            }
            if (lightFlicker != null)
            {
                lightFlicker.SetActive(false);
            }
            if (hallwayLight != null)
            {
                hallwayLight.SetActive(false);
            }
        }
    }
}
