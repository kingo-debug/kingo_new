using UnityEngine;
using Photon.Pun;
using System.IO;


public class InventoryInstantiate : MonoBehaviour
        {

    [SerializeField]
    private Transform i1;
    [SerializeField]
    private Transform i2;
    [SerializeField]
    private Transform i3;
    [SerializeField]
    private Transform i4;

    public GameObject MeleeWeapon;
    public GameObject BackupWeapon;
    public GameObject PrimaryWeapon;
    public GameObject HeavyWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        if(transform.parent.GetComponent<PhotonView>().IsMine)
        {
            #region Spawn Weapons
            MeleeWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("Melee")), new Vector3(0, 0, 0), Quaternion.identity);
            BackupWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("Backup")), new Vector3(0, 0, 0), Quaternion.identity);
            PrimaryWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("Primary")), new Vector3(0, 0, 0), Quaternion.identity);
            HeavyWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("Heavy")), new Vector3(0, 0, 0), Quaternion.identity);
            #endregion
        }



    }

}
