using UnityEngine;

public class ReloadAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Clip;
    public Transform Hand;
    public WeaponStatus weapon;



    public void DetatchClip()
    {
        Clip = weapon.CurrentWeapon.transform.GetChild(0).transform.Find("CLIP PLACE").transform.GetChild(0);
        Clip.transform.parent = Hand;
        Clip.transform.localPosition = new Vector3(0, 0, 0);
        Clip.transform.localRotation = new Quaternion(0, 0, 0,0);
    }

    public void AttachClip()
    {
        Clip.transform.parent = weapon.CurrentWeapon.transform.GetChild(0).transform.Find("CLIP PLACE");
        Clip.transform.localPosition = new Vector3(0, 0, 0);
        Clip.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
