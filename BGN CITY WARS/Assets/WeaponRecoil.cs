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
    public float SpreadSpeed;
    public bool PlayerAiming = false;
    public float ReticleMaxSpread;

    public float DebugRectmag;
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
        DebugRectmag = recttransform.sizeDelta.x;
    
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
        }
     else if(PlayerAiming && recttransform.sizeDelta.magnitude < AimingReticleSize )
        {
             recttransform.sizeDelta += new Vector2(RecoilCoolDownSpeed, RecoilCoolDownSpeed) * Time.deltaTime;
        }
    }
    public void AddReticleRecoid()
    {
        recttransform.sizeDelta += new Vector2(ReticleRecoilAmount, ReticleRecoilAmount);
    }
}
