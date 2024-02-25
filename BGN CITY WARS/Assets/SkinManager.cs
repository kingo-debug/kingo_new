using UnityEngine;
using Photon.Pun;
using System.IO;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer SkinMesh;
    public string EquippedSkin;
    private InitiateData data;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
        PV = GetComponent<PhotonView>();
        Invoke("SpawnSkin", 0.1f);
    }

    public void SpawnSkin()
    {
        if(PV.IsMine)
        {

            SkinMesh.material =  Resources.Load<GameObject>(Path.Combine(("skins"), data.EquippedSkin)).GetComponent<SkinData>().SkinMaterial;

            EquippedSkin = data.EquippedSkin;



        }
    }
}
