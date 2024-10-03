using UnityEngine;
using Photon.Pun;

public class ArmorOwnerLocate : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    private Transform Upperpart;
    private Transform LowerPart;
    private GameObject PlayerOwner;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(transform.childCount>0)
        {
            Upperpart = transform.GetChild(0);
            LowerPart = transform.GetChild(1);
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

            }



            if (Upperpart!= null)
            {
                Upperpart.transform.parent = PlayerOwner.GetComponent<ArmorManager>().TargetUpperBody; ; Upperpart.transform.localPosition = new Vector3(0, 0, 0); Upperpart.transform.localRotation = new Quaternion(0, 0, 0, 0); Upperpart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
                LowerPart.transform.parent = PlayerOwner.GetComponent<ArmorManager>().TargetLowerBody; ; LowerPart.transform.localPosition = new Vector3(0, 0, 0); LowerPart.transform.localRotation = new Quaternion(0, 0, 0, 0); LowerPart.transform.localScale = new Vector3(0.01590658f, 0.01780851f, 0.01282499f);
            }
      
        }
    }

}
