using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedShopItems : MonoBehaviour


{

    public List<string> OwnedWeapons;
    public List<string> OwnedSkins;
    public Dictionary<string, string> Weaponinventory = new Dictionary<string, string>();


    [Space(10)]
    [Header("Equipped Inventory")]
    public string EquippedMelee;
    public string EquippedBackup;
    public string EquippedPrimary;
    public string EquippedHeavy;

    [Space(10)]
    [Header("Equipped SKIN")]
    public string EquippedSkin;

    // Start is called before the first frame update
    void Start()
    {

        LoadWeapons();
        LoadSkins();
    }

    public void SaveWeapons()
    {
        ES3.Save<List<string>>("OwnedWeapons", OwnedWeapons);
    }

    public void LoadWeapons()
    {
        OwnedWeapons =ES3.Load<List<string>>("OwnedWeapons", OwnedWeapons); // load owned weapons

        EquippedMelee = ES3.Load<string>("MeleeEquip");   // load inventory
        EquippedBackup = ES3.Load<string>("BackupEquip");
        EquippedPrimary = ES3.Load<string>("PrimaryEquip");
        EquippedHeavy = ES3.Load<string>("HeavyEquip");
    }


    public void LoadSkins()
    {
        OwnedSkins = ES3.Load<List<string>>("OwnedSkins", OwnedSkins);

        EquippedSkin = ES3.Load<string>("CurrentSkin");
    }


    public void SaveSkins()
    {
        ES3.Save<List<string>>("OwnedSkins", OwnedSkins);
    }

}