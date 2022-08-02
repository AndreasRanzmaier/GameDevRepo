using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ShaderHandler : MonoBehaviour
{
    public Material effectMaterial;
    private void OnRenderImage(RenderTexture source, RenderTexture destinaton)
    {
        Graphics.Blit(source, destinaton, effectMaterial);
    }
}
