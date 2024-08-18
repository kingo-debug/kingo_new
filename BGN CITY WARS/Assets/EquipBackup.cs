using UnityEngine;
using UnityEngine.UI;
public class EquipBackup : MonoBehaviour
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
        states = GameObject.Find("ScrollView-Backup").GetComponent<EquippedState>();

        if (buy.WeaponID == data.EquippedBackup)

        {
            Equip();
        }
    }
    public void Equip()
    {


        Debug.Log(data.EquippedBackup = buy.WeaponID);
        ES3.Save<string>("BackupEquip", buy.WeaponID);

        #region Selection Bar UI Icon
        CategorySelectPFP.GetComponent<Image>().sprite = transform.parent.parent.transform.Find("Weapon icon").GetComponent<Image>().sprite;
        #endregion
        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // set new equip button
        #endregion



    }

}
