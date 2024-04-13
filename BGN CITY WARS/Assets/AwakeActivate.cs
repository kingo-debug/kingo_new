using Photon.Pun;
using UnityEngine;

public class AwakeActivate : MonoBehaviour
{
    [SerializeField]
    private PhotonView PV;
    [SerializeField]
    private GameObject ObjToActivate;
    private void Awake()
    {
     if(PV.IsMine)
        {
            ObjToActivate. gameObject.SetActive ( true);
        }
    }
}
