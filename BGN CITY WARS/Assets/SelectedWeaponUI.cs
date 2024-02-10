using UnityEngine;
using UnityEngine.UI;

public class SelectedWeaponUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image PFP;
    [SerializeField]
    private WeaponStatus  status;

    void Start()
    {
        UpdateSelectedWeraponUI();
    }


    public void UpdateSelectedWeraponUI()
    {
        PFP.sprite = status.CurrentWeapon.GetComponent<WeaponType>().WeaponPFP;
    }
}
