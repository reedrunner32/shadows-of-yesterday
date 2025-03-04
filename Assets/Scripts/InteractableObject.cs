using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Material originalMaterial;
    public Material glowMaterial; // Assign a yellow-glow material in the inspector
    private bool isHovered = false;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (isHovered && Input.GetKeyDown(KeyCode.E))
        {
            PickUpObject();
        }
    }

    public void OnHoverEnter()
    {
        GetComponent<Renderer>().material = glowMaterial;
        isHovered = true;
    }

    public void OnHoverExit()
    {
        GetComponent<Renderer>().material = originalMaterial;
        isHovered = false;
    }

    private void PickUpObject()
    {
        Debug.Log("Object Picked Up: " + gameObject.name);
        gameObject.SetActive(false); // Hide object (adjust based on your pickup system)
    }
}
