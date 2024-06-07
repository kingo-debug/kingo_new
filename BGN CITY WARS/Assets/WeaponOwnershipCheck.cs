using UnityEngine;

public class WeaponOwnershipCheck : MonoBehaviour
{

    [SerializeField]
    private bool Owned;

    private BuyWeapon Buy;

    private OwnedShopItems shopitems;
    private EquipWeapon equip;
    private GameObject Equipped;



    void OnEnable()
    {
        Buy = transform.Find("STATE").transform.GetChild(0).gameObject.GetComponent<BuyWeapon>();
        equip = transform.Find("STATE").transform.GetChild(1).gameObject.GetComponent<EquipWeapon>();
        Equipped = transform.Find("STATE").transform.GetChild(2).gameObject;
        shopitems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        CheckOwnerShipAndEquip();
    }


    private void Start()
    {
        CheckOwnerShipAndEquip();
    }


    public void CheckOwnerShipAndEquip()
    {
        if (shopitems.OwnedWeapons.Contains(Buy.WeaponID))
        {
            Owned = true;
            Buy.gameObject.SetActive(false);

        }
        else
        {
            equip.gameObject.SetActive(false);
            Equipped. SetActive(false);
            Buy.gameObject.SetActive(true);
            Owned = false;
        }

        if (Buy.WeaponID == shopitems.EquippedBackup ||
       Buy.WeaponID == shopitems.EquippedMelee ||
       Buy.WeaponID == shopitems.EquippedPrimary ||
       Buy.WeaponID == shopitems.EquippedHeavy && Owned==true)
        {
           
                
                equip.Equip();
                
         
        }
          else if (Owned)
        {
        
            equip.gameObject.SetActive(true);
            Equipped.SetActive(false);
            Buy.gameObject.SetActive(false);

        }
       
    
        
    }
  
}
