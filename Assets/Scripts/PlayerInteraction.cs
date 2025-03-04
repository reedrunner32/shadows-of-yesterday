using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    private InteractableObject lastHoveredObject;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionRange))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            if (interactable)
            {
                if (lastHoveredObject != interactable)
                {
                    if (lastHoveredObject) lastHoveredObject.OnHoverExit();
                    interactable.OnHoverEnter();
                    lastHoveredObject = interactable;
                }
            }
            else if (lastHoveredObject)
            {
                lastHoveredObject.OnHoverExit();
                lastHoveredObject = null;
            }
        }
        else if (lastHoveredObject)
        {
            lastHoveredObject.OnHoverExit();
            lastHoveredObject = null;
        }
    }
}
