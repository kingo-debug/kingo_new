using UnityEngine;

public class WeaponOwnershipCheck : MonoBehaviour
{

    [SerializeField]
    private bool Owned;

    private BuyWeapon Buy;

    private InitiateData data;
    private OwnedShopItems shopitems;
    private EquipWeapon equip;
    private GameObject Equipped;



    void OnEnable()
    {
        CheckOwnerShipAndEquip();
    }
    // Start is called before the first frame update
    [SerializeField]
    private void Start()
    {
        Buy = transform.Find("STATE").transform.GetChild(0).gameObject.GetComponent<BuyWeapon>();
        equip = transform.Find("STATE").transform.GetChild(1).gameObject.GetComponent<EquipWeapon>();
        Equipped = transform.Find("STATE").transform.GetChild(2).gameObject;
        data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
        shopitems = GameObject.Find("SHOP MENU").GetComponent<OwnedShopItems>();


        CheckOwnerShipAndEquip();
    }
    public void CheckOwnerShipAndEquip()
    {
        if (shopitems.OwnedWeapons.Contains(Buy.WeaponID))
        {
            Owned = true;
        }

        if (Buy.WeaponID == data.EquippedBackup ||
       Buy.WeaponID == data.EquippedMelee ||
       Buy.WeaponID == data.EquippedPrimary ||
       Buy.WeaponID == data.EquippedHeavy)
        {
            equip.Equip();
        }
          else
        {
            equip.gameObject.SetActive(true);
            Equipped.SetActive(false);
            
        }
    }
  
}
