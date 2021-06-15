using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenEffects : MonoBehaviour
{
    //public Material fishmaterial;
    public Material Oldmaterial;

    // Start is called before the first frame update
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Graphics.Blit(source, destination, fishmaterial);
        Graphics.Blit(source, destination, Oldmaterial);
    }
}
