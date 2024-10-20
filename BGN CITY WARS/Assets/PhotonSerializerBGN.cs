using UnityEngine;
using Photon.Pun;


public class PhotonSerializerBGN : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;
    [Header("Player Status")]
    public string PlayerNickName;
    public int HP;
    public int Shield;
    public int Stamina;
    public string LeagueLogoPath;



    [Header("Skin")]
    public string SkinID;

    [Header("Weapon")]
    public int InventoryTrack;
    public int CurrentWeaponType;

    [Header("IK")]
    public float LookIK;
    public float AimIK;

    [Header("Weapon Status")]
    public bool Fired;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        PlayerNickName = ES3.Load<string>("PlayerName");
        PhotonNetwork.NickName = PlayerNickName;

    }






    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("streamIsCalled");
        if (stream.IsWriting && photonView != null)
        {
            stream.SendNext(PlayerNickName); //PlayerNickName

            stream.SendNext(HP); //HPSync

            stream.SendNext(Shield); //ShieldSync

            stream.SendNext(SkinID); //SkinSync
        
            stream.SendNext(InventoryTrack);    //InventoryTrackerSync

          stream.SendNext(CurrentWeaponType);    //SelectedWeaponTypeTrackerSync

            stream.SendNext(LookIK);    //lookIK

            stream.SendNext(AimIK);    //AimIK

            stream.SendNext(Fired);    //FiredBool

            stream.SendNext(LeagueLogoPath); //PlayerLeagueLogoPath


        }
        else
        {

            PlayerNickName = (string)stream.ReceiveNext(); // other player PlayerNickName

            HP = (int)stream.ReceiveNext(); // other player HP

            Shield = (int)stream.ReceiveNext(); // other player Shield

            SkinID = (string)stream.ReceiveNext();  //other player SkinID

            InventoryTrack = (int)stream.ReceiveNext(); // other player Inventory

            CurrentWeaponType = (int)stream.ReceiveNext(); //other player CurrentWeaponType

            LookIK = (float)stream.ReceiveNext(); // other player LookIK

            AimIK = (float)stream.ReceiveNext(); // other player aIMik

            Fired = (bool)stream.ReceiveNext(); // other player FiredBool

            LeagueLogoPath = (string)stream.ReceiveNext(); // other player PlayerNickName


        }
    }


    










   

}
