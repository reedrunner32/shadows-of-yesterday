using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayerOnInteract : MonoBehaviour
{
    public PlayableDirector timelineDirector; // Reference to the Timeline's PlayableDirector

    private void OnEnable()
    {
        // Subscribe to the Interact event
        PickableObject.OnInteract += PlayTimeline;
    }

    private void OnDisable()
    {
        // Unsubscribe from the Interact event
        PickableObject.OnInteract -= PlayTimeline;
    }

    private void PlayTimeline(GameObject interactedObject)
    {
        // Check if the interacted object is the one this script is attached to
        if (interactedObject == gameObject && timelineDirector != null)
        {
            timelineDirector.Play();
        }
    }
}