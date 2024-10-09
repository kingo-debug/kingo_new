using UnityEngine;
using Photon.Pun;
using System.IO;
public class ArmorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CurrentBodyArmor;
    public Transform TargetMainBody;
    public Transform TargetShoulderR;
  public Transform TargetShoulderL;
    private PhotonView PV;
    private PhotonSerializerBGN photonSerializer;
    [SerializeField] private ArmorUICheck checkarmor;
    

    void Start()
    {
        PV = GetComponent<PhotonView>();
        Invoke("SpawnArmor", 0.01f);
    }

    public void SpawnArmor()
    {
        if (PV.IsMine)
        {
            if (ES3.Load<string>("CurrentArmor")!= "NoArmor")
            {
                //spawn Body armor
                CurrentBodyArmor = PhotonNetwork.Instantiate(Path.Combine("Armors", ES3.Load<string>("CurrentArmor")), new Vector3(0, 0, 0), Quaternion.identity);

                //assign BODY PART ARMOR
                Transform BodyPart = CurrentBodyArmor.transform.Find("BODY PART ARMOR").transform;
                BodyPart.transform.parent = TargetMainBody; BodyPart.transform.localPosition = new Vector3(0, 0, 0); BodyPart.transform.localRotation = new Quaternion(0, 0, 0, 0); BodyPart.transform.localScale = new Vector3(0.01590658f,0.01780851f,0.01282499f);

                // Check if the "SHOULDER RIGHT" part exists before assigning LowerPartArmor
                Transform rightPart = CurrentBodyArmor?.transform.Find("SHOULDER_R");
                Transform LeftPart = CurrentBodyArmor?.transform.Find("SHOULDER_L");

                if (rightPart != null && LeftPart != null)
                {
                    rightPart.transform.parent = TargetShoulderR;
                    rightPart.transform.localPosition = Vector3.zero; // Instead of new Vector3(0, 0, 0)
                    rightPart.transform.localRotation = Quaternion.identity; // Instead of new Quaternion(0, 0, 0, 0)
                    rightPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);

                    LeftPart.transform.parent = TargetShoulderL;
                    LeftPart.transform.localPosition = Vector3.zero; // Instead of new Vector3(0, 0, 0)
                    LeftPart.transform.localRotation = Quaternion.identity; // Instead of new Quaternion(0, 0, 0, 0)
                    LeftPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }

                checkarmor.ShowShieldBar();
            }
              





        }
        else
        {
        
            

        }
    }

    public void RemoveArmor()
    {
        checkarmor.HideShieldBar();
    }
}
