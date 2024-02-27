using UnityEngine;

public class WaterAnimateUV : MonoBehaviour
{
    [SerializeField]
    private Vector2 WaveSpeed;
    private Material BaseMaterial;

    void Start()
    {
        BaseMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        BaseMaterial.mainTextureOffset += WaveSpeed * Time.deltaTime;
    }
}
