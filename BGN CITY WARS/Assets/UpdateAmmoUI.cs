using UnityEngine;
using TMPro;

public class UpdateAmmoUI : MonoBehaviour
{
    [SerializeField]
    PlayerActionsVar playerActions;
    [SerializeField]
    WeaponStatus weaponStatus;
    [SerializeField]
    private TextMeshProUGUI text;

    void Start()
    {
        UpdateAmmoUIDisplay();
    }

 
   public void UpdateAmmoUIDisplay()
    {
        text.text = (weaponStatus.CurrentClip.ToString() + "/" + weaponStatus.TotalAmmo.ToString());
    }

    public void HideAmmoUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void DisplayAmmoUI()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
