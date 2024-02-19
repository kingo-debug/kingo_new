using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class EquipSkin : MonoBehaviour
{
    private BuySkin buy;
    private InitiateData data;
    private RefreshEquippedStates states;
    private GameObject SkinItem;

    private GameObject EquippedButton;

    private void Start()
    {
        SkinItem = transform.parent.parent.gameObject;
        buy = transform.parent.GetChild(0).GetComponent<BuySkin>();
        data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
        EquippedButton = transform.parent.GetChild(2).gameObject;
    

    }

    private void OnEnable()
    {
        states = transform.parent.parent.parent.GetComponent<RefreshEquippedStates>();
        states.EquippedItem.transform.Find("STATE").GetChild(2).gameObject.SetActive(false);    //deactivate equipped button
        states.EquippedItem.transform.transform.Find("STATE").GetChild(1).gameObject.SetActive(true);    //activate equip button
        states.EquippedItem = transform.parent.parent.gameObject;    //update equipped item

        if (buy.SkinID == data.EquippedSkin)
   
        {
            Equip();
        }
    }
    public void Equip()
    {
    
            Debug.Log(data.EquippedSkin = buy.SkinID);
            data.SaveStats();
        #region Button Toggles
        EquippedButton.SetActive(true);

        states.EquippedItem.transform.Find("STATE").GetChild(2).gameObject.SetActive(false);    //deactivate equipped button
        states.EquippedItem.transform.transform.Find("STATE").GetChild(1).gameObject.SetActive(true);    //activate equip button

        states.EquippedItem = transform.parent.parent.gameObject;    //update equipped item
        #endregion




        gameObject.SetActive(false);




        
       

    }


}
