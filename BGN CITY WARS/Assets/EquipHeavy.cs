using UnityEngine;

public class EquipHeavy : MonoBehaviour
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
        states = GameObject.Find("ScrollView-Heavy").GetComponent<EquippedState>();

        if (buy.WeaponID == data.EquippedHeavy)

        {
            Equip();
        }
    }
    public void Equip()
    {


        Debug.Log(data.EquippedHeavy = buy.WeaponID);
        ES3.Save<string>("HeavyEquip", buy.WeaponID);

        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // set new equip button
        #endregion




    }
}
