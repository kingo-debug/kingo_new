using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EquipWeapon : MonoBehaviour
{
    private BuyWeapon buy;
    private InitiateData data;

    [SerializeField]
    private string category;


    private void Start()
    {
        buy = transform.parent.Find("BUY").GetComponent<BuyWeapon>();
        data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();

    }
    public  void Equip()
    {
        // Check if the category exists in the dictionary
        if (data.Weaponinventory.ContainsKey(category))
        {
            // If the category already exists, update its value
            Debug.Log(data.Weaponinventory[category] = buy.WeaponID);
            Debug.Log(data.Weaponinventory.TryGetValue(category, out string value));
            Debug.Log(value);
            data.SaveStats();

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
