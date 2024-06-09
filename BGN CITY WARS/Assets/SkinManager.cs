using UnityEngine;
using Photon.Pun;
using System.IO;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer SkinMesh;
    public string EquippedSkin;
    private OwnedShopItems data;
    private PhotonView PV;
    private PhotonSerializerBGN photonSerializer;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        PV = GetComponent<PhotonView>();
        photonSerializer = GetComponent<PhotonSerializerBGN>();
        Invoke("SpawnSkin", 0.02f);
    }

    public void SpawnSkin()
    {
        if(PV.IsMine)
        {

            SkinMesh.material =  Resources.Load<GameObject>(Path.Combine(("skins"), ES3.Load<string>("CurrentSkin"))).GetComponent<SkinData>().SkinMaterial;

            EquippedSkin = data.EquippedSkin;

        }
        else
        {
            SkinMesh.material = Resources.Load<GameObject>(Path.Combine(("skins"),photonSerializer.SkinID)).GetComponent<SkinData>().SkinMaterial;

            EquippedSkin = photonSerializer.SkinID;

        }
    }
}
