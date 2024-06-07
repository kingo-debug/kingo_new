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
  
        if(transform.root.GetComponent<PhotonView>().IsMine)
        {
            #region Spawn Weapons
            MeleeWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("CurrentMelee")), new Vector3(0, 0, 0), Quaternion.identity);            MeleeWeapon.transform.parent = i1;       MeleeWeapon.transform.localPosition = new Vector3(0, 0, 0);  MeleeWeapon.transform.localScale = new Vector3(1, 1, 1); MeleeWeapon.transform.localRotation = new Quaternion(0, 0, 0, 0);
            BackupWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("CurrentBackup")), new Vector3(0, 0, 0), Quaternion.identity);          BackupWeapon.transform.parent = i2;     BackupWeapon.transform.localPosition = new Vector3(0, 0, 0); BackupWeapon.transform.localScale = new Vector3(1, 1, 1); BackupWeapon. transform.localRotation = new Quaternion(0, 0, 0, 0);
            PrimaryWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("CurrentPrimary")), new Vector3(0, 0, 0), Quaternion.identity);        PrimaryWeapon.transform.parent = i3;     PrimaryWeapon.transform.localPosition = new Vector3(0, 0, 0); PrimaryWeapon.transform.localScale = new Vector3(1, 1, 1); PrimaryWeapon.transform.localRotation = new Quaternion(0, 0, 0, 0);
            HeavyWeapon = PhotonNetwork.Instantiate(Path.Combine("weapons", ES3.Load<string>("CurrentHeavyy")), new Vector3(0, 0, 0), Quaternion.identity);              HeavyWeapon.transform.parent = i4;       HeavyWeapon.transform.localPosition = new Vector3(0, 0, 0); HeavyWeapon.transform.localScale = new Vector3(1, 1, 1); HeavyWeapon.transform.localRotation = new Quaternion(0, 0, 0, 0);


            #endregion
        }



    }

}
