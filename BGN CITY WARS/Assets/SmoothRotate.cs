using UnityEngine;

public class SmoothRotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 RotateDirection;
    [SerializeField]
    private float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateDirection, Speed*Time.deltaTime);

       
    }
}
