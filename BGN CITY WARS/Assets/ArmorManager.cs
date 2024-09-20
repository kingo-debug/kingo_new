using UnityEngine;
using Photon.Pun;
using System.IO;
public class ArmorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CurrentBodyArmor;
    [SerializeField] private Transform TargetUpperBody;
    [SerializeField] private Transform TargetLowerBody;
    private PhotonView PV;
    private PhotonSerializerBGN photonSerializer;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        photonSerializer = GetComponent<PhotonSerializerBGN>();
        Invoke("SpawnArmor", 0.01f);
    }

    private void SpawnArmor()
    {
        if (PV.IsMine)
        {
            CurrentBodyArmor = PhotonNetwork.Instantiate(Path.Combine("Armors", ES3.Load<string>("CurrentArmor")), new Vector3(0, 0, 0), Quaternion.identity);
            Transform LowerPart = CurrentBodyArmor.transform.Find("UPPER PART ARMOR").transform;
            LowerPart.transform.parent = TargetUpperBody; LowerPart.transform.localPosition = new Vector3(0, 0, 0); LowerPart.transform.localRotation = new Quaternion(0, 0, 0, 0);//MeleeWeapon.transform.localScale = new Vector3(1, 1, 1); 




        }
        else
        {
       //     CurrentBodyArmor = Resources.Load<GameObject>(Path.Combine(("skins"), photonSerializer.SkinID));
        
            

        }
    }
}
