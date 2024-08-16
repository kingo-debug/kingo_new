using UnityEngine;
using Photon.Pun;
using RootMotion.FinalIK;

public class NetWorkPlayerSync : MonoBehaviour
{

    private PhotonView PV;
    private PhotonSerializerBGN PunSerializer;
    private PlayerActionsVar playerActions;
    [SerializeField]
    private GameObject[] NotMine;
    [SerializeField]
    private Component[] NotMineComponents;
    private LookAtIK lookik;
    private AimIK aimik;
    private OwnedShopItems Data;
    private SkinManager skinManager;
    private MainCameraController CameraControler;
    private Camera Playercamera;
    private TakeDamage takedamage;

    private bool DisabledBefore = false;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        PunSerializer = GetComponent<PhotonSerializerBGN>();
        aimik = GetComponent<AimIK>();
        lookik = GetComponent<LookAtIK>();
        playerActions = GetComponent<PlayerActionsVar>();
       // Data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        skinManager = GetComponent<SkinManager>();
        Playercamera = transform.parent.transform.GetChild(1).GetChild(0).GetComponent<Camera>();
        CameraControler = transform.parent.transform.GetChild(1).GetChild(0).GetComponent<MainCameraController>();
        takedamage = GetComponent<TakeDamage>();


        if (!PV.IsMine)
        {
            #region Disable all not mine objects
            foreach (GameObject item in NotMine)
            {
                item.SetActive(false);
            }
            #endregion
            #region Disable all not mine Components
            foreach (Component item in NotMineComponents)
            {
                if (item is Behaviour)
                {
                    ((Behaviour)item).enabled = false;
                }
            }
            Playercamera.enabled = false;
            CameraControler.enabled = false;
            #endregion

        }


    }

    // Update is called once per frame
    void Update()
    {
        #region Set variables anyway

        #endregion

        if (PV.IsMine)
        {
            bool SkinSet = false;
                if(!SkinSet)
            {
                PunSerializer.SkinID = ES3.Load<string>("CurrentSkin");
                SkinSet = true;
            }
          
            PunSerializer.InventoryTrack = playerActions.InventoryTrack;
            PunSerializer.CurrentWeaponType = playerActions.Weapontype;
            PunSerializer.Fired = playerActions.Fired;
            PunSerializer.LookIK = lookik.GetIKSolver().IKPositionWeight;
            PunSerializer.AimIK = aimik.GetIKSolver().IKPositionWeight;
            PunSerializer.HP = takedamage.HP;

        }
        else
        {
            aimik.solver.IKPositionWeight = PunSerializer.AimIK;

            lookik.solver.IKPositionWeight = PunSerializer.LookIK;

            skinManager.EquippedSkin = PunSerializer.SkinID;

            takedamage.HP = PunSerializer.HP;





        }

    }


    [PunRPC]
    public void DisablePlayer()
    {
        transform.root.gameObject.SetActive(false);
    }
    [PunRPC]
    public void EnablePlayer()
    {
        transform.root.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        PV.RPC("DisablePlayer", RpcTarget.Others);
        DisabledBefore = true;
    }
    public void OnEnable()
    {
        if(DisabledBefore)
        {
            PV.RPC("EnablePlayer", RpcTarget.Others);
        }
       
    }
}
