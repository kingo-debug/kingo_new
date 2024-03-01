using UnityEngine;
using TMPro;

public class UpdateAmmoUI : MonoBehaviour
{
    [SerializeField]
    PlayerActionsVar playerActions;
    [SerializeField]
    private TextMeshProUGUI text;
 
   public void UpdateAmmoUIDisplay(int Clip, int TotalAmmo )
    {
        text.text = (Clip.ToString() + "/" + TotalAmmo.ToString());
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
