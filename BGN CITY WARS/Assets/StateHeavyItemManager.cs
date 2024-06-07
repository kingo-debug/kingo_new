using UnityEngine;

public class StateHeavyItemManager : MonoBehaviour
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
        states = GameObject.Find("ScrollView-Heavy").GetComponent<EquippedState>();
        ownedShopItems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        RefreshStates();
    }
    public void RefreshStates()
    {
        if (ownedShopItems.OwnedWeapons.Contains(BuyState.GetComponent<BuyWeapon>().WeaponID))  // check owns
        {
            BuyState.SetActive(false);  // does own, Disable buy button


            if (BuyState.GetComponent<BuyWeapon>().WeaponID == ownedShopItems.EquippedHeavy)  // check equip
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
