using UnityEngine;
using Photon.Pun;
using System.IO;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer SkinMesh;
    public string EquippedSkin;
    private PhotonView PV;
    private PhotonSerializerBGN photonSerializer;
    public GameObject SkinItem;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        photonSerializer = GetComponent<PhotonSerializerBGN>();
        Invoke("SpawnSkin", 0.02f);
    }

    public void SpawnSkin()
    {
        if(PV.IsMine)
        {
            SkinItem = Resources.Load<GameObject>(Path.Combine(("skins"), ES3.Load<string>("CurrentSkin")));
            SkinMesh.material =  Resources.Load<GameObject>(Path.Combine(("skins"), ES3.Load<string>("CurrentSkin"))).GetComponent<SkinData>().SkinMaterial;

            EquippedSkin = ES3.Load<string>("CurrentSkin");

        }
        else
        {
            SkinItem = Resources.Load<GameObject>(Path.Combine(("skins"), photonSerializer.SkinID));

           SkinMesh.material = Resources.Load<GameObject>(Path.Combine(("skins"),photonSerializer.SkinID)).GetComponent<SkinData>().SkinMaterial;

            EquippedSkin = photonSerializer.SkinID;

        }
    }
}
