using UnityEngine;
using UnityEngine.UI;
using ControlFreak2;

public class SelectedWeaponUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image PFP;
    [SerializeField]
    private Image UiFire;
    [SerializeField]
    private TouchButtonSpriteAnimator  uifire;
    [SerializeField]
    private WeaponStatus  status;

    void Start()
    {
        UpdateSelectedWeraponUI();
    }


    public void UpdateSelectedWeraponUI()
    {
        PFP.sprite = status.CurrentWeapon.GetComponent<WeaponType>().WeaponPFP;
        UpdateFireButtonUI();
    }

    void UpdateFireButtonUI()
    {
        uifire.SetSprite (status.CurrentWeapon.GetComponent<WeaponType>().WeaponFireUi);
        
    }
}
