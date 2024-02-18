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
    void OnEnable()
    {
        LoadEquipped();

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
          ES3.Save(EquipCategory, EquippedItem);
        Debug.Log(EquipCategory);
     //   Debug.Log(ES3.Load(EquipCategory));
    }
    public void LoadEquipped()
    {
        EquippedItem = ES3.Load<GameObject>(EquipCategory);
    }
}