using System.IO;
using UnityEngine;

public class ArmorSpawnOffline : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CurrentBodyArmor;
    [SerializeField] private Transform MainBodyVest;
    [SerializeField] private Transform Shoulder_R;
    [SerializeField] private Transform Shoulder_L;
    private PhotonSerializerBGN photonSerializer;

    void Start()
    {
        Invoke("SpawnArmor", 0.01f);
    }

    public void SpawnArmor()
    {

        {
            if (ES3.Load<string>("CurrentArmor") != "NoArmor")
            {
                // Load the GameObject from the Resources folder before instantiating
                string armorPath = Path.Combine("Armors", ES3.Load<string>("CurrentArmor"));
                GameObject armorPrefab = Resources.Load<GameObject>(armorPath);

                if (armorPrefab != null)
                {
                    if(MainBodyVest.childCount>0)
                    {
                        Destroy(MainBodyVest.GetChild(0));
                    }
                    // Instantiate the prefab at the given position and rotation
                    CurrentBodyArmor = GameObject.Instantiate(armorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    Debug.LogError($"Armor prefab not found at path: {armorPath}");
                }


                //assign MainBodyPart
                Transform MainBody = CurrentBodyArmor.transform.Find("BODY PART ARMOR").transform;
                MainBody.transform.parent = MainBodyVest; MainBody.transform.localPosition = new Vector3(0, 0, 0); MainBody.transform.localRotation = new Quaternion(0, 0, 0, 0); MainBody.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                MainBody.gameObject.layer = 18;

                if(CurrentBodyArmor.transform.childCount>1)
                {
                    //assign SHOULDER R
                    Transform ShoulderR = CurrentBodyArmor.transform.Find("SHOULDER_R").transform;
                    ShoulderR.transform.parent = Shoulder_R; ShoulderR.transform.localPosition = new Vector3(0, 0, 0); ShoulderR.transform.localRotation = new Quaternion(0, 0, 0, 0); ShoulderR.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                    ShoulderR.gameObject.layer = 18;

                    //assign SHOULDER L
                    Transform ShoulderL = CurrentBodyArmor.transform.Find("SHOULDER_L").transform;
                    ShoulderL.transform.parent = Shoulder_R; ShoulderL.transform.localPosition = new Vector3(0, 0, 0); ShoulderL.transform.localRotation = new Quaternion(0, 0, 0, 0); ShoulderL.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                    ShoulderL.gameObject.layer = 18;
                }

            }






        }



    }
     
    }
