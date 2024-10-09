using UnityEngine;
using Photon.Pun;

public class ArmorOwnerLocate : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    [SerializeField] private Transform BodyVestPart;
    [SerializeField]private Transform RightShoulderCover;
    [SerializeField] private Transform LeftShoulderCover;
    private GameObject PlayerOwner;
    void Start()
    {
        PV = GetComponent<PhotonView>();

        AssignParent();
    }

    void AssignParent()
    {
        if (!PV.IsMine) // locate other Player as parent for armor
        {
            if (PV.ViewID != 0) // not main menu mode
            {
                PlayerOwner = GameObject.Find(PV.Owner.ToString()).transform.GetChild(0).gameObject;
                #region AssignArmorForOtherPlayersInGame
                if (BodyVestPart != null) // check if has Vest
                {
                    BodyVestPart.transform.parent = PlayerOwner.GetComponent<ArmorManager>().TargetMainBody; ; BodyVestPart.transform.localPosition = new Vector3(0, 0, 0); BodyVestPart.transform.localRotation = new Quaternion(0, 0, 0, 0); BodyVestPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }
                if (RightShoulderCover != null) // check if has R shoulder
                {
                    RightShoulderCover.transform.parent = PlayerOwner.GetComponent<ArmorManager>().TargetShoulderR; ; RightShoulderCover.transform.localPosition = new Vector3(0, 0, 0); RightShoulderCover.transform.localRotation = new Quaternion(0, 0, 0, 0); RightShoulderCover.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }

                if (LeftShoulderCover != null) // check if has L shoulder
                {
                    LeftShoulderCover.transform.parent = PlayerOwner.GetComponent<ArmorManager>().TargetShoulderR; ; LeftShoulderCover.transform.localPosition = new Vector3(0, 0, 0); LeftShoulderCover.transform.localRotation = new Quaternion(0, 0, 0, 0); LeftShoulderCover.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }
            }
            #endregion
            else
            {
                #region AssignArmorForPlayerMainMenu
                if (BodyVestPart != null) // check if has Vest
                {
                    BodyVestPart.transform.parent = GameObject.Find("MAIN ARMOR PLACEMENT").transform;  BodyVestPart.transform.localPosition = new Vector3(0, 0, 0); BodyVestPart.transform.localRotation = new Quaternion(0, 0, 0, 0); BodyVestPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }
                if (RightShoulderCover != null) // check if has R shoulder
                {
                    RightShoulderCover.transform.parent = GameObject.Find("SHOULDER ARMOR R PLACEMENT").transform;  RightShoulderCover.transform.localPosition = new Vector3(0, 0, 0); RightShoulderCover.transform.localRotation = new Quaternion(0, 0, 0, 0); RightShoulderCover.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }

                if (LeftShoulderCover != null) // check if has L shoulder
                {
                    LeftShoulderCover.transform.parent = GameObject.Find("SHOULDER ARMOR L PLACEMENT").transform; LeftShoulderCover.transform.localPosition = new Vector3(0, 0, 0); LeftShoulderCover.transform.localRotation = new Quaternion(0, 0, 0, 0); LeftShoulderCover.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                }
            }
            #endregion
        }

    }



}

    


