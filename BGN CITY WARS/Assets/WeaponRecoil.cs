using UnityEngine;


public class WeaponRecoil : MonoBehaviour
{
    // Start is called before the first frame update
    private WeaponType weapontype;
    private Transform Reticle;
    public Vector2 DefaultReticleSize;
    private void OnEnable()
    {
        weapontype = GetComponent<WeaponType>();
        Reticle = GameObject.Find("CROSSHAIRS").transform.GetChild(weapontype.ReticleType).GetChild(0);
        Reticle.GetComponent<RectTransform>().sizeDelta = DefaultReticleSize;
    }
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
