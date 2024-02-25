using UnityEngine.UI;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    private BuyWeapon buy;
    private InitiateData data;
    private RefreshEquippedStates states;
    private GameObject WeaponItem;



    [SerializeField]
    private string category;
    [SerializeField]
    private Transform CategorySelectPFP;

    private GameObject EquippedButton;

    private void OnEnable()
    {
        if(WeaponItem ==null || buy == null || states ==null | EquippedButton ==null | data ==null)
        {
            WeaponItem = transform.parent.parent.gameObject;
            buy = transform.parent.GetChild(0).GetComponent<BuyWeapon>();
            data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
            EquippedButton = transform.parent.GetChild(2).gameObject;
            states = transform.parent.parent.parent.GetComponent<RefreshEquippedStates>();
        }


        if (buy.WeaponID == data.EquippedBackup ||
       buy.WeaponID == data.EquippedMelee ||
       buy.WeaponID == data.EquippedPrimary ||
       buy.WeaponID == data.EquippedHeavy )
        {
            Equip();
        }
    }
    public void Equip()
    {
        // Check if the category exists in the dictionary
        if (data.Weaponinventory.ContainsKey(category))
        {
            // If the category already exists, update its value
            Debug.Log(data.Weaponinventory[category] = buy.WeaponID);
            Debug.Log(data.Weaponinventory.TryGetValue(category, out string value));
            Debug.Log(value);
            data.SaveStats();
            #region Button Toggles
            EquippedButton.SetActive(true);

            states.EquippedItem.transform.Find("STATE").GetChild(2).gameObject.SetActive(false);    //deactivate equipped button
            states.EquippedItem.transform.transform.Find("STATE").GetChild(1).gameObject.SetActive(true);    //activate equip button
          
            states.EquippedItem = transform.parent.parent.gameObject;    //update equipped item
            states.EquippedItem.transform.Find("STATE").GetChild(2).gameObject.SetActive(true);    //activate equipped button
            states.EquippedItem.transform.transform.Find("STATE").GetChild(1).gameObject.SetActive(false);    //deactivate equip button
            #endregion
            CategorySelectPFP.Find("EQUIP PFP").GetComponent<Image>().sprite = WeaponItem.transform.Find("Weapon icon").GetComponent<Image>().sprite;
            gameObject.SetActive(false);




        }
        else
        {
            // If the category doesn't exist, add it to the dictionary
            data.Weaponinventory.Add(category, buy.WeaponID);
        }
   
    }


}
