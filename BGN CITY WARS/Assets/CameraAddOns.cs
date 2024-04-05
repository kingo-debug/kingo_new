
using UnityEngine;

public class CameraAddOns : MonoBehaviour
{
    private camera2 cam2;
    private Transform PlayerRoot;

    private float DefaultRightOffset = 0.56f;
    [SerializeField]
    private float MaxRightOffset = .3f;
    [SerializeField]
    private float MinRightOffset = 0.17f;
    [SerializeField]
    private float Smoothness = 5f;
    // Start is called before the first frame update
    void Start()
    {
        cam2 = GetComponent<camera2>();
        PlayerRoot = transform.root.GetChild(0).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam2.rightOffset = Mathf.Lerp(cam2.rightOffset, Mathf.Clamp(PlayerRoot.transform.position.y / transform.position.y * DefaultRightOffset, MinRightOffset, MaxRightOffset), Time.deltaTime*Smoothness);


    }
}
