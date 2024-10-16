using UnityEngine;

public class StateVehicleItemManager : MonoBehaviour
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
        states = GameObject.Find("vehicle window").GetComponent<EquippedState>();
        ownedShopItems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        RefreshStates();
    }

    public void RefreshStates()
    {
        if (ownedShopItems.OwnedVehicles.Contains(BuyState.GetComponent<BuyVehicle>().VehicleID)) // check owns
        {
            BuyState.SetActive(false); // does own, disable buy button

            if (BuyState.GetComponent<BuyVehicle>().VehicleID == ownedShopItems.EquippedVehicle) // check equip
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
            BuyState.SetActive(true); // does not own, enable buy button, disable other states
            EquipState.SetActive(false);
            EquippedState.SetActive(false);
        }
    }
}
