using UnityEngine;
using Photon.Pun;
public class NetWorkParentWeapon : MonoBehaviour
{
    private PhotonView PV;
    [SerializeField]
    private int InventorySlot;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (transform.parent == null && PV.IsMine==false)
        {
            transform.parent = GameObject.Find(PV.Owner.ToString()).transform.GetChild(0).GetComponent<InventoryManager>().Inventory[InventorySlot].transform;

        }
    }

}
