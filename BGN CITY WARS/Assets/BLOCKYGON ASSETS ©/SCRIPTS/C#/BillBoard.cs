using Photon.Pun;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public bool DisableForME = false;
    public bool DisableForOthers = false;
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation* Vector3.up);
    }
    private void Start()
    {
        if(DisableForME)
        {
            if (transform.root.GetComponent<PhotonView>().IsMine)
            {
                gameObject.SetActive(false);
            }
        }
       
        if(DisableForOthers)
        {
            if (transform.root.GetComponent<PhotonView>().IsMine==false)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
