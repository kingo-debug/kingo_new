using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedShopItems : MonoBehaviour


{

    public List<string> OwnedWeapons;
    public List<string> OwnedSkins;
    public List<string> OwnedArmors;
    public List<string> OwnedVehicles;
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

    [Space(10)]
    [Header("Equipped Armor")]
    public string EquippedArmor;

    [Space(10)]
    [Header("Equipped Vehicle")]
    public string EquippedVehicle;
    // Start is called before the first frame update
    void Start()
    {

        LoadWeapons();
        LoadSkins();
        LoadArmors();
        LoadVehicles();
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

    public void LoadArmors()
    {
        OwnedArmors = ES3.Load<List<string>>("OwnedArmors", OwnedArmors);

        EquippedArmor = ES3.Load<string>("CurrentArmor");
    }
    public void SaveArmors()
    {
        ES3.Save<List<string>>("OwnedArmors", OwnedArmors);
    }

    public void SaveVehicles()
    {
        ES3.Save<List<string>>("OwnedVehicles", OwnedVehicles);
    }

    public void LoadVehicles()
    {
        OwnedVehicles = ES3.Load<List<string>>("OwnedVehicles", OwnedVehicles);

        EquippedVehicle = ES3.Load<string>("CurrentVehicle");
    }
}