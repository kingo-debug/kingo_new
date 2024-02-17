using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshWeaponStates : MonoBehaviour

{
    public GameObject EquippedItem;
    public string EquipCategory;
    private void OnDisable()
    {
        SaveEquipped();

    }
    void OnEnable()
    {
        EquippedItem =ES3.Load<GameObject>(EquipCategory);
   

    }
    public void RefreshAllWeaponStates()
    {
        SaveEquipped();
     //   foreach (Transform weapon in gameObject.transform)
       // {
      //      weapon.GetComponent<WeaponOwnershipCheck>().CheckOwnerShipAndEquip();
    //    }
    }

   public void SaveEquipped()
    {
        ES3.Save<GameObject>(EquipCategory,EquippedItem);
    }
    public void LoadEquipped()
    {
        EquippedItem = ES3.Load<GameObject>(EquipCategory);
    }
}
