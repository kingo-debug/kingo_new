using UnityEngine;
using UnityEngine.UI;
public class BackupStateManager : MonoBehaviour
{
    [SerializeField]
    GameObject BuyState;
    [SerializeField]
    GameObject EquipState;
    [SerializeField]
    GameObject EquippedState;
    [SerializeField]
    private Transform CategorySelectPFP;

    EquippedState states;
    OwnedShopItems ownedShopItems;

    // Start is called before the first frame update
    void Start()
    {
        RefreshStates();

    }

    private void OnEnable()
    {
        states = GameObject.Find("ScrollView-Backup").GetComponent<EquippedState>();
        ownedShopItems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        RefreshStates();
    }
    public void RefreshStates()
    {
        if (ownedShopItems.OwnedWeapons.Contains(BuyState.GetComponent<BuyWeapon>().WeaponID))  // check owns
        {
            BuyState.SetActive(false);  // does own, Disable buy button


            if (BuyState.GetComponent<BuyWeapon>().WeaponID == ownedShopItems.EquippedBackup)  // check equip
            {
                EquippedState.SetActive(true);
                EquipState.SetActive(false);

                #region refresh states last equipped
                states.CurrentlyEquipped = transform.GetChild(2).gameObject;
                #endregion
                #region Selection Bar UI Icon
                CategorySelectPFP.GetComponent<Image>().sprite = transform.parent.transform.Find("Weapon icon").GetComponent<Image>().sprite;
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
