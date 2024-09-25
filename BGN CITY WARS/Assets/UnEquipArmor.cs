using UnityEngine.UI;
using UnityEngine;

public class UnEquipArmor : MonoBehaviour
{
    private BuyArmor buy;
    private OwnedShopItems data;
    private EquippedState states;

    [SerializeField]
    private Transform EquipButton;
    //[SerializeField]
    //private Transform CategorySelectPFP;



    private void Start()
    {
        buy = transform.parent.transform.GetChild(0).gameObject.GetComponent<BuyArmor>();
        data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        states = GameObject.Find("ScrollView-BodyArmor").GetComponent<EquippedState>();

      
    }
    public void UnEquip()
    {


        Debug.Log(data.EquippedArmor = "NoArmor");
        ES3.Save<string>("CurrentArmor","NoArmor");

        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // set new equip button
        #endregion

        EquipButton.gameObject.SetActive(true);

        //#region Selection Bar UI Icon
        //CategorySelectPFP.GetComponent<Image>().sprite = transform.parent.parent.Find("Armor icon").GetComponent<Image>().sprite;
        //#endregion
    }

}
