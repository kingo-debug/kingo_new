using UnityEngine;


public class WeaponRecoil : MonoBehaviour
{
    // Start is called before the first frame update
    private WeaponType weapontype;
    private Transform Reticle;
    public Vector2 DefaultReticleSize;
    public Vector2 AimingReticleSize;
    public Vector2 ReticleRecoilAmount;
    private RectTransform recttransform;
    public float RecoilCoolDownSpeed;
    public float SpreadSpeed;
    public bool PlayerAiming = false;
    private void OnEnable()
    {
        weapontype = GetComponent<WeaponType>();
        Reticle = GameObject.Find("CROSSHAIRS").transform.GetChild(weapontype.ReticleType).GetChild(0);
        Reticle.GetComponent<RectTransform>().sizeDelta = DefaultReticleSize;
    }
    void Start()
    {
        recttransform = Reticle.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
     if (recttransform.sizeDelta !=  DefaultReticleSize&& !PlayerAiming)
        {
            recttransform.sizeDelta = Vector2.Lerp(recttransform.sizeDelta, DefaultReticleSize,Time.deltaTime* RecoilCoolDownSpeed);
            
        }
     else
        {
            recttransform.sizeDelta = Vector2.Lerp(recttransform.sizeDelta, DefaultReticleSize + AimingReticleSize, Time.deltaTime * 7f);

        }
    }
    public void AddReticleRecoid()
    {
        recttransform.sizeDelta -= ReticleRecoilAmount * Time.deltaTime*SpreadSpeed;
    }
}
