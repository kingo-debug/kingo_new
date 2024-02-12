using Photon.Pun;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
 
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation* Vector3.up);
    }
    private void Start()
    {
        if(transform.root.GetComponent<PhotonView>().IsMine)
        {
            gameObject.SetActive(false);
        }
    }
}
