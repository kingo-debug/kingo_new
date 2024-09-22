using UnityEngine;

public class FollowTargetSnappy : MonoBehaviour
{
    [SerializeField] private Transform Target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position!= Target.position)
        {
            transform.position = Target.position;
        }
    }
}
