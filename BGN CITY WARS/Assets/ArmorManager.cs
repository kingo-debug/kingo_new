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
            if (ES3.Load<string>("CurrentArmor")!= "NoArmor")
            {
                //spawn Body armor
                CurrentBodyArmor = PhotonNetwork.Instantiate(Path.Combine("Armors", ES3.Load<string>("CurrentArmor")), new Vector3(0, 0, 0), Quaternion.identity);
                
            //assign upperPartArmor
            Transform Upperpart = CurrentBodyArmor.transform.Find("UPPER PART ARMOR").transform;
            Upperpart.transform.parent = TargetUpperBody; Upperpart.transform.localPosition = new Vector3(0, 0, 0); Upperpart.transform.localRotation = new Quaternion(0, 0, 0, 0);Upperpart.transform.localScale = new Vector3(0.01590658f,0.01780851f,0.01282499f);

            //assign LowerPartArmor
            Transform LowerPart = CurrentBodyArmor.transform.Find("LOWER PART AMROR").transform;
            LowerPart.transform.parent = TargetLowerBody; LowerPart.transform.localPosition = new Vector3(0, 0, 0); LowerPart.transform.localRotation = new Quaternion(0, 0, 0, 0); LowerPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);

            }
              





        }
        else
        {
       //     CurrentBodyArmor = Resources.Load<GameObject>(Path.Combine(("skins"), photonSerializer.SkinID));
        
            

        }
    }
}
