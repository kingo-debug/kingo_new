using UnityEngine;
using UnityEngine.UI;
public class EquipPrimary : MonoBehaviour
{
    private BuyWeapon buy;
    private OwnedShopItems data;
    private EquippedState states;

    [SerializeField]
    private Transform CategorySelectPFP;



    private void Start()
    {
        buy = transform.parent.transform.GetChild(0).gameObject.GetComponent<BuyWeapon>();
        data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        states = GameObject.Find("ScrollView-Primary").GetComponent<EquippedState>();

        if (buy.WeaponID == data.EquippedPrimary)

        {
            Equip();
        }
    }
    public void Equip()
    {


        Debug.Log(data.EquippedPrimary = buy.WeaponID);
        ES3.Save<string>("PrimaryEquip", buy.WeaponID);

        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // set new equip button
        #endregion

        #region Selection Bar UI Icon
        CategorySelectPFP.GetComponent<Image>().sprite = transform.parent.parent.Find("Weapon icon").GetComponent<Image>().sprite;
        #endregion


    }
}
