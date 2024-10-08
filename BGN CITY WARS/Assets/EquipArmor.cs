using UnityEngine.UI;
using UnityEngine;

public class EquipArmor : MonoBehaviour
{
    private BuyArmor buy;
    private OwnedShopItems data;
    private EquippedState states;

    [SerializeField]
    private Transform CategorySelectPFP;

    [SerializeField]
    private Sprite CategoryUNSelectPFP;

    [SerializeField]
    private Transform MainArmorPlacement;



    private void Start()
    {
        buy = transform.parent.transform.GetChild(0).gameObject.GetComponent<BuyArmor>();
        data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        states = GameObject.Find("ScrollView-BodyArmor").GetComponent<EquippedState>();

        if (buy.ArmorID == data.EquippedArmor)

        {
            Equip();
        }
        else
        {
            #region Selection Bar UI Icon
            CategorySelectPFP.GetComponent<Image>().sprite = CategoryUNSelectPFP;
            #endregion
        }
    }
    public void Equip()
    {


        Debug.Log(data.EquippedArmor = buy.ArmorID);
        ES3.Save<string>("CurrentArmor", buy.ArmorID);

        if (MainArmorPlacement.childCount > 0)
        {
            Destroy(MainArmorPlacement.GetChild(0).transform.gameObject);
        }

        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // set new equip button
        #endregion

        #region Selection Bar UI Icon
        CategorySelectPFP.GetComponent<Image>().sprite = transform.parent.parent.Find("Armor icon").GetComponent<Image>().sprite;
        #endregion
    }

}
