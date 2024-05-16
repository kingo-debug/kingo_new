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
    [SerializeField]
    private float FadeSensitivity = 0.2f;
    [SerializeField]
    private float DistanceToStart = 1.5f;
    [Space(20)]
    [Header("Scaling")]
    [SerializeField]
    private bool ScaleTargetPoint = true;
    [SerializeField]
    private float DistanceToScale = 0.35f;
    private Vector3 DefaultTargetPointScale;

    void Start()
    {
        Invoke("SetMaterial", 0.5f);
        DefaultTargetPointScale = TargetPoint.localScale;
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(TargetCamera.position, TargetPoint.position);

        if (distance < DistanceToStart)
        {
            SetTransparent();
        }
        else SetOpaque();

        if (distance < DistanceToScale && TargetPoint.localScale!= new Vector3(0, 0, 0))
        {
            SetScaleTargetPoint();
        }
        else if (TargetPoint.localScale !=DefaultTargetPointScale)
        {
            ResetTargetPointScale();
        }
            
    }



    // Set the material to opaque
void SetOpaque()
    {
  
        material.SetFloat("_Surface", 0); // 0 is for opaque
        material.SetFloat("_AlphaClip", 1); // Enable alpha clipping if needed
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        material.EnableKeyword("_SURFACE_TYPE_OPAQUE");
        material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
        material.SetColor("_BaseColor", new Color(1, 1, 1, 1));
    }


    // Set the material to transparent
void SetTransparent()
    {
 
        material.SetFloat("_Surface", 1); // 1 is for transparent
        material.SetFloat("_AlphaClip", 0); // Disable alpha clipping
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        material.DisableKeyword("_SURFACE_TYPE_OPAQUE");
        material.SetColor("_BaseColor", new Color(1,1, 1, distance*FadeSensitivity));
    }


    void SetMaterial()
    {
        material = Mesh.material;
    }

    void SetScaleTargetPoint()
    {

        TargetPoint.localScale = new Vector3(0, 0, 0);
    }
    void ResetTargetPointScale()
    {
        TargetPoint.localScale = DefaultTargetPointScale;
    }
}
