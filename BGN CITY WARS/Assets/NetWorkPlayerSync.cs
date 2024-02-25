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
    void Start()
    {
        PV = GetComponent<PhotonView>();
        PunSerializer = GetComponent<PhotonSerializerBGN>();
        aimik = GetComponent<AimIK>();
        lookik = GetComponent<LookAtIK>();
        playerActions = GetComponent<PlayerActionsVar>();

        if(!PV.IsMine)
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
            #endregion

        }


    }

    // Update is called once per frame
    void Update()
    {
        #region Set variables anyway
        PunSerializer.LookIK = (int)lookik.solver.IKPositionWeight;
        PunSerializer.AimIK = (int)aimik.solver.IKPositionWeight;
        #endregion

        if (PV.IsMine)
        {
            PunSerializer.SkinID = Data.EquippedSkin;
            PunSerializer.InventoryTrack = playerActions.InventoryTrack;
            PunSerializer.CurrentWeaponType = playerActions.Weapontype;
            PunSerializer.Fired = playerActions.Fired;


        }
        else
        {
            aimik.solver.IKPositionWeight = PunSerializer.AimIK;

            lookik.solver.IKPositionWeight = PunSerializer.LookIK;
    


        }

    }
}
