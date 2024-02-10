using UnityEngine;
using UnityEngine.UI;

public class WeaponUIFrame : MonoBehaviour
{
    [SerializeField]
    private PlayerActionsVar Actions;
    [SerializeField]
    private Transform inventory;
    [SerializeField]
    private int InventorytrackNumber;
    [SerializeField]
    private Image  WeaponPFP;





    public  void SelectWeapon()
    {
        Actions.InventoryTrack = InventorytrackNumber;

    }

    private void OnEnable()
    {
        WeaponPFP.sprite = inventory.GetChild(InventorytrackNumber).transform.GetChild(0).GetComponent<WeaponType>().WeaponPFP;

     
    }

}
