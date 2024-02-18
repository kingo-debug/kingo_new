using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedShopItems : MonoBehaviour


{

    public List<string> OwnedWeapons;
    public List<string> OwnedSkins;
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
        OwnedWeapons =ES3.Load<List<string>>("OwnedWeapons", OwnedWeapons);
    }


    public void LoadSkins()
    {
        OwnedSkins = ES3.Load<List<string>>("OwnedSkins", OwnedSkins);
    }


    public void SaveSkins()
    {
        ES3.Save<List<string>>("OwnedSkins", OwnedSkins);
    }

}