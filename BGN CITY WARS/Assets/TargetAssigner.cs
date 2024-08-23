using UnityEngine;

public class TargetAssigner : MonoBehaviour
{
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MINI MAP CAM").gameObject.GetComponent<SmoothFollower>().target = Target;
    }

}
