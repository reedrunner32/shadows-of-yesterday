using UnityEngine;

public class ChangePaintings : MonoBehaviour
{
    public GameObject painting1; 
    public Texture newTexture1;
    public GameObject painting2;
    public Texture newTexture2;
    public GameObject painting3;
    public Texture newTexture3;
    public GameObject painting4;
    public Texture newTexture4;

    private void OnEnable()
    {
        // Subscribe to the Interact event
        PickableObject.OnInteract += ChangePainting;
    }

    private void OnDisable()
    {
        // Unsubscribe from the Interact event
        PickableObject.OnInteract -= ChangePainting;
    }

    private void ChangePainting(GameObject interactedObject)
    {
        if (painting1 != null && newTexture1 != null)
        {
            Renderer renderer = painting1.GetComponent<Renderer>();
            Material materialInstance = new Material(renderer.material);
            materialInstance.SetTexture("_MainTex", newTexture1);

            materialInstance.mainTextureScale = new Vector2(1.3f, 1.0f);

            renderer.material = materialInstance;
        }
        if (painting2 != null && newTexture2 != null)
        {
            Renderer renderer = painting2.GetComponent<Renderer>();
            Material materialInstance = new Material(renderer.material);
            materialInstance.SetTexture("_MainTex", newTexture2);
            renderer.material = materialInstance;
        }
        if (painting3 != null && newTexture3 != null)
        {
            Renderer renderer = painting3.GetComponent<Renderer>();
            Material materialInstance = new Material(renderer.material);
            materialInstance.SetTexture("_MainTex", newTexture3);
            renderer.material = materialInstance;
        }
        if (painting4 != null && newTexture4 != null)
        {
            Renderer renderer = painting4.GetComponent<Renderer>();
            Material materialInstance = new Material(renderer.material);
            materialInstance.SetTexture("_MainTex", newTexture4);
            renderer.material = materialInstance;
        }
    }
}
