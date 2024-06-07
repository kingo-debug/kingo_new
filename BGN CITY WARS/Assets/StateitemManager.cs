using UnityEngine;

public class StateitemManager : MonoBehaviour
{
    [SerializeField]
    GameObject BuyState;
    [SerializeField]
    GameObject EquipState;
    [SerializeField]
    GameObject EquippedState;

    EquippedState states;
    OwnedShopItems ownedShopItems;

    // Start is called before the first frame update
    void Start()
    {
        RefreshStates();
      
    }

    private void OnEnable()
    {
        states = GameObject.Find("skin window").GetComponent<EquippedState>();
        ownedShopItems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        RefreshStates();
    }
    public void RefreshStates()
    {
        if (ownedShopItems.OwnedSkins.Contains(BuyState.GetComponent<BuySkin>().SkinID))  // check owns
        {
            BuyState.SetActive(false);  // does own, Disable buy button


           if (BuyState.GetComponent<BuySkin>().SkinID == ownedShopItems.EquippedSkin)  // check equip
            {
                EquippedState.SetActive(true);
                EquipState.SetActive(false);

                #region refresh states last equipped
                states.CurrentlyEquipped = transform.GetChild(2).gameObject;
                #endregion


            }
            else
            {
                EquippedState.SetActive(false);
                EquipState.SetActive(true);
            }

        }
        else
        {
            BuyState.SetActive(true);// does not own, enable buy button,disable other states.
            EquipState.SetActive(false);
            EquippedState.SetActive(false);
        }
          
    }

}
