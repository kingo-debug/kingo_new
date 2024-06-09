using UnityEngine;
using Photon.Pun;
public class DieEfX : MonoBehaviour

{
 
 [SerializeField]
 private GameObject[] ObjectsToHide;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public void HideOut()
    {
        if (!PV.IsMine)
        {
            foreach (GameObject item in ObjectsToHide)
        {
            item.gameObject.SetActive(false);
        }
        }

    }
    public void UnHide()
    {
        if (!PV.IsMine)
    {
            foreach (GameObject item in ObjectsToHide)
        {
            item.gameObject.SetActive(true);
        }
    }
   }
}
