using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFadeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SkinnedMeshRenderer Mesh;
    [SerializeField]
    private Transform TargetCamera;
    [SerializeField]
    private Transform TargetPoint;
    [SerializeField]
    private float distance;
    private Material material;
    void Start()
    {
        material = Mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(TargetCamera.position, TargetPoint.position);

        if (distance < 1.5)
        {
            SetTransparent();
        }
        else SetOpaque();
    }



    // Set the material to opaque
    public void SetOpaque()
    {
        material.SetFloat("_Surface", 0); // 0 is for opaque
        material.SetFloat("_AlphaClip", 1); // Enable alpha clipping if needed
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        material.EnableKeyword("_SURFACE_TYPE_OPAQUE");
        material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
    }


    // Set the material to transparent
    public void SetTransparent()
    {
        material.SetFloat("_Surface", 1); // 1 is for transparent
        material.SetFloat("_AlphaClip", 0); // Disable alpha clipping
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        material.DisableKeyword("_SURFACE_TYPE_OPAQUE");
    }


}
