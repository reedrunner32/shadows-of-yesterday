using UnityEngine;

[ExecuteInEditMode]
public class DoubleVisionEffect : MonoBehaviour
{
    public Material doubleVisionMaterial;
    public float offsetSpeed = 1.0f;
    public float maxOffset = 0.05f;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (doubleVisionMaterial != null)
        {
            // Update the material's offset property
            float offset = Mathf.Sin(Time.time * offsetSpeed) * maxOffset;
            doubleVisionMaterial.SetFloat("_Offset", offset);

            // Apply the material to the camera's output
            Graphics.Blit(source, destination, doubleVisionMaterial);
        }
        else
        {
            // If no material is assigned, just copy the source to the destination
            Graphics.Blit(source, destination);
        }
    }
}