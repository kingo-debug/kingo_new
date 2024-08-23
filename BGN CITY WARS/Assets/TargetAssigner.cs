using UnityEngine;

public class TargetAssigner : MonoBehaviour
{
    public Transform Target;
    private SmoothFollower SF;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("ReTarget", 0.1f);
    }
    void Start()
    {
        SF = GameObject.Find("MINI MAP CAM").gameObject.GetComponent<SmoothFollower>();
        SF.target = Target;
    }
    void ReTarget()
    {
        SF.target = Target;
    }

}
