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
    private InitiateData Data;
    private SkinManager skinManager;
    private camera2 Camera2;
    private Camera Playercamera;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PunSerializer = GetComponent<PhotonSerializerBGN>();
        aimik = GetComponent<AimIK>();
        lookik = GetComponent<LookAtIK>();
        playerActions = GetComponent<PlayerActionsVar>();
        Data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
        skinManager = GetComponent<SkinManager>();
        Playercamera = transform.parent.transform.GetChild(1).GetChild(0).GetComponent<Camera>();
        Camera2 = transform.parent.transform.GetChild(1).GetChild(0).GetComponent<camera2>();



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
            Camera2.enabled = false;
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

            PunSerializer.SkinID = Data.EquippedSkin;
            PunSerializer.InventoryTrack = playerActions.InventoryTrack;
            PunSerializer.CurrentWeaponType = playerActions.Weapontype;
            PunSerializer.Fired = playerActions.Fired;
            PunSerializer.LookIK = lookik.GetIKSolver().IKPositionWeight;
            PunSerializer.AimIK = aimik.GetIKSolver().IKPositionWeight;

        }
        else
        {
            aimik.solver.IKPositionWeight = PunSerializer.AimIK;

            lookik.solver.IKPositionWeight = PunSerializer.LookIK;

            skinManager.EquippedSkin = PunSerializer.SkinID;




        }

    }
}
