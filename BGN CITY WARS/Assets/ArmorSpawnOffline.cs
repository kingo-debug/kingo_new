using System.IO;
using UnityEngine;

public class ArmorSpawnOffline : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CurrentBodyArmor;
    [SerializeField] private Transform TargetUpperBody;
    [SerializeField] private Transform TargetLowerBody;
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
                    if(TargetUpperBody.childCount>0)
                    {
                        Destroy(TargetUpperBody.GetChild(0));
                    }
                    // Instantiate the prefab at the given position and rotation
                    CurrentBodyArmor = GameObject.Instantiate(armorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                }
                else
                {
                    Debug.LogError($"Armor prefab not found at path: {armorPath}");
                }


                //assign upperPartArmor
                Transform Upperpart = CurrentBodyArmor.transform.Find("UPPER PART ARMOR").transform;
                Upperpart.transform.parent = TargetUpperBody; Upperpart.transform.localPosition = new Vector3(0, 0, 0); Upperpart.transform.localRotation = new Quaternion(0, 0, 0, 0); Upperpart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                Upperpart.gameObject.layer = 18;

                //assign LowerPartArmor
                Transform LowerPart = CurrentBodyArmor.transform.Find("LOWER PART AMROR").transform;
                LowerPart.transform.parent = TargetLowerBody; LowerPart.transform.localPosition = new Vector3(0, 0, 0); LowerPart.transform.localRotation = new Quaternion(0, 0, 0, 0); LowerPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                LowerPart.gameObject.layer = 18;
            }






        }



    }
     
    }
