using UnityEngine;
using Photon.Pun;
public class NetWorkParentWeapon : MonoBehaviour
{
    private PhotonView PV;
    private WeaponType weapontype;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        weapontype = GetComponent<WeaponType>();
        if (transform.parent == null && PV.IsMine==false)
        {
            transform.parent = GameObject.Find(PV.Owner.ToString()).transform.GetChild(0).GetComponent<InventoryManager>().Inventory[weapontype.Weapontype].transform;

        }
    }

}
