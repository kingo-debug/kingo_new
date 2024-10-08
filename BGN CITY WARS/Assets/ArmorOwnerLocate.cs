using UnityEngine;
using Photon.Pun;

public class ArmorOwnerLocate : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    private Transform BodyVestPart;
    private Transform RightShoulderCover;
    private Transform LeftShoulderCover;
    private GameObject PlayerOwner;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(transform.childCount>0)
        {
            BodyVestPart = transform.GetChild(0);
            if (transform.childCount > 1)
            {
                RightShoulderCover = transform.GetChild(1);
                LeftShoulderCover = transform.GetChild(1);
            }
      
        }

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


    private void OnDestroy()
    {
        Destroy(BodyVestPart.gameObject);
        Destroy(RightShoulderCover.gameObject);
        Destroy(LeftShoulderCover.gameObject);
    }


}

    


