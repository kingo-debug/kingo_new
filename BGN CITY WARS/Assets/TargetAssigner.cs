using UnityEngine;
using Photon.Pun;
public class TargetAssigner : MonoBehaviour
{
    public Transform Target;
    private SmoothFollower SF;
    [SerializeField]
    private PhotonView PV;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if(!PV.IsMine)
        {
            this.enabled = false;
        }
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
