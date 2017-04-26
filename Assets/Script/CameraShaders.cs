using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaders : MonoBehaviour {

    private Material blackScreenShader;

    private void Awake()
    {
        blackScreenShader = new Material(Shader.Find("Hidden/blackScreen"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, blackScreenShader);
    }

}
