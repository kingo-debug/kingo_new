using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshWeaponStates : MonoBehaviour

{
    public GameObject EquippedItem;
    public string EquipCategory;
    private InitiateData data;
    private void OnDisable()
    {
        SaveEquipped();

    }
  
    public void RefreshAllWeaponStates()
    {
        SaveEquipped();
    
    }

    public void SaveEquipped()
    {
        ES3.Save(EquipCategory, EquippedItem);
     
   
    }
   
}