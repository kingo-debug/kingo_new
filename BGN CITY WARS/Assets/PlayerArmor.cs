using UnityEngine;
using Photon.Pun;

public class PlayerArmor : MonoBehaviour
{

    public int ArmorAmount;
    public string ArmorName;
    private TakeDamage takedamage;
    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = transform.parent.GetComponent<PhotonView>();
        Invoke("SetArmor", 0.05f);

    }

    public void SetArmor()
    {     
        if(transform.root.GetComponent<PhotonView>().IsMine)
        {
            takedamage = transform.root.GetChild(0).GetComponent<TakeDamage>();
            takedamage.Shield = ArmorAmount;
            takedamage.DelayBar2Refresh();
        }
        else
        {
            takedamage.Shield = transform.root.GetChild(0).GetComponent<PhotonSerializerBGN>().Shield;
        }
    }

}
