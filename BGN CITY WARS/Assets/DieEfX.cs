using UnityEngine;
using Photon.Pun;
public class DieEfX : MonoBehaviour

{
    [SerializeField]
    private GameObject[] ObjectsToHide;



    private void Awake()
    {
        PhotonView PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            this.enabled = false;
        }
    }
    public void HideOut()
    {

        foreach (GameObject item in ObjectsToHide)
        {
            item.gameObject.SetActive(false);
        }

    }
    public void UnHide()
    {

        foreach (GameObject item in ObjectsToHide)
        {
            item.gameObject.SetActive(true);
        }
    }
}
