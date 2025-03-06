using UnityEngine;

[ExecuteInEditMode]
public class PixelationEffect : MonoBehaviour
{
    public Material pixelationMaterial;
    [Range(1, 100)] public int pixelSize = 4;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (pixelationMaterial != null)
        {
            pixelationMaterial.SetFloat("_PixelSize", pixelSize);
            Graphics.Blit(src, dest, pixelationMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}