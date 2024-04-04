using UnityEngine;


public class WeaponRecoil : MonoBehaviour
{
    // Start is called before the first frame update
    private WeaponType weapontype;
    private Transform Reticle;
    public float DefaultReticleSize;
    public float AimingReticleSize;
    public float ReticleRecoilAmount;
    private RectTransform recttransform;
    public float RecoilCoolDownSpeed;
    public bool PlayerAiming = false;
    public float ReticleMaxSpread;

    private void OnEnable()
    {
        weapontype = GetComponent<WeaponType>();
        Reticle = GameObject.Find("CROSSHAIRS").transform.GetChild(weapontype.ReticleType).GetChild(0);
        Reticle.GetComponent<RectTransform>().sizeDelta = new Vector2( DefaultReticleSize, DefaultReticleSize);
    }
    void Start()
    {
        recttransform = Reticle.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
            if (recttransform.sizeDelta.x != DefaultReticleSize&& !PlayerAiming)
        {
            recttransform.sizeDelta -= new Vector2(RecoilCoolDownSpeed, RecoilCoolDownSpeed) ;

            if(recttransform.sizeDelta.x > ReticleMaxSpread) // Limit Max
            {
                recttransform.sizeDelta = new Vector2(ReticleMaxSpread, ReticleMaxSpread);
            }
            else if(recttransform.sizeDelta.x < DefaultReticleSize) // LimitMin)
            {
                recttransform.sizeDelta = new Vector2(DefaultReticleSize, DefaultReticleSize);
            }
        } //DEFAULT STATE
            else if(recttransform.sizeDelta.x != AimingReticleSize && PlayerAiming)
        {
            recttransform.sizeDelta -= new Vector2(RecoilCoolDownSpeed, RecoilCoolDownSpeed);

            if (recttransform.sizeDelta.x > ReticleMaxSpread) // Limit Max
            {
                recttransform.sizeDelta = new Vector2(ReticleMaxSpread, ReticleMaxSpread);
            }
            else if (recttransform.sizeDelta.x < DefaultReticleSize) // LimitMin)
            {
                recttransform.sizeDelta = new Vector2(AimingReticleSize, AimingReticleSize);
            }
        }  // AIMING STATE
    }
    public void AddReticleRecoid()
    {
        recttransform.sizeDelta += new Vector2(ReticleRecoilAmount, ReticleRecoilAmount);
    }
}
