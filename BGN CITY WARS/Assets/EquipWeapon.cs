using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EquipWeapon : MonoBehaviour
{
    private BuyWeapon buy;
    private InitiateData data;
    private RefreshWeaponStates states;


    [SerializeField]
    private string category;

    private GameObject EquippedButton;

    private void Start()
    {
        buy = transform.parent.GetChild(0).GetComponent<BuyWeapon>();
        data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
        EquippedButton = transform.parent.GetChild(2).gameObject;
        states = transform.parent.parent.parent.GetComponent<RefreshWeaponStates>();

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
            #endregion
            gameObject.SetActive(false);



        }
        else
        {
            // If the category doesn't exist, add it to the dictionary
            data.Weaponinventory.Add(category, buy.WeaponID);
        }
   
    }
    public void DebugTest()
    {
        Debug.Log(data.Weaponinventory.TryGetValue(category, out string value));
        Debug.Log(value);

    }

}
