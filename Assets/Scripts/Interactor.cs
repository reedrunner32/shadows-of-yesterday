using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    void Interact();
    void OnHover(bool isLooking);
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource; // Assign your Camera Transform here
    public float InteractRange = 3f;

    private IInteractable lastHoveredObject = null;

    void Update()
    {
        bool hitInteractable = false;

        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            IInteractable interactObj = hitInfo.collider.GetComponent<IInteractable>();

            if (interactObj != null)
            {
                interactObj.OnHover(true); // Apply yellow glow
                hitInteractable = true;

                if (lastHoveredObject != interactObj && lastHoveredObject != null)
                {
                    lastHoveredObject.OnHover(false); // Remove glow from the last object
                }

                lastHoveredObject = interactObj;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
        }

        // If no interactable object was hit this frame, remove glow from the last object
        if (!hitInteractable && lastHoveredObject != null)
        {
            lastHoveredObject.OnHover(false);
            lastHoveredObject = null;
        }
    }
}
