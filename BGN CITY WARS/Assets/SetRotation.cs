using UnityEngine;

public class SetRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform target;
    private Quaternion PreviousRotation;

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation!= PreviousRotation)
        transform.rotation = target.transform.rotation;
        PreviousRotation = transform.rotation;
    }
}
