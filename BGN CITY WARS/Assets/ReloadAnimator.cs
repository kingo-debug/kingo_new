using UnityEngine;
using Photon.Pun;
public class ReloadAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Clip;
    public Transform Hand;
    public WeaponStatus weapon;
    private Transform FoundWeapon;
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public void DetatchClip()
    {
  if(PV.IsMine)
        {
            Clip = weapon.CurrentWeapon.transform.GetChild(0).transform.Find("CLIP PLACE").transform.GetChild(0);
            FoundWeapon = weapon.CurrentWeapon.transform;
            Clip.transform.parent = Hand;
            Clip.transform.localPosition = new Vector3(0, 0, 0);
            Clip.transform.localRotation = new Quaternion(0, 0, 0, 0);
            // Clip.transform.localScale = new Vector3(1, 1, 1);
        }



    }

    public void AttachClip()
    {
        if (PV.IsMine)
        {
            Clip.transform.parent = FoundWeapon.GetChild(0).transform.Find("CLIP PLACE");
            Clip.transform.localPosition = new Vector3(0, 0, 0);
            Clip.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Clip.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
