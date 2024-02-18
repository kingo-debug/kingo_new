using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;
using System.Collections.Generic;



public class InitiateData : MonoBehaviour
{
    private bool NewPlayer = true;
    public int BGNCoins;
    public int CurrentXP;
    public int CurrentLevel;
    public string ConvertedXP;
    [Space(10)]
    [Header("Equipped Inventory")]
    public string EquippedMelee;
    public string EquippedBackup;
    public string EquippedPrimary;
    public string EquippedHeavy;


    public Dictionary<string, string> Weaponinventory = new Dictionary<string, string>();



    public string EquippedSkin;
    [SerializeField]
    private TextMeshProUGUI CoinsTextUI;

    [SerializeField]
    private TextMeshProUGUI CurrentLevelUI;

    [SerializeField]
    private TextMeshProUGUI ConvertedXPUi;

    private void OnEnable()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString("UserStats_PlayerName");
    }

    private void Awake()
    {
        // data initiatation.
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            int RandomNums = Random.Range(1000, 10000);

            //set all initiatated data.
            #region Initiate UserStats
            PlayerPrefs.SetString("UserStats_PlayerName", "Player" + RandomNums.ToString());


            //ESsave for security
            BGNCoins = 500;
            ES3.Save("BgnCoins", BGNCoins);




            #endregion

            #region GameSettings
            //Audio
            PlayerPrefs.SetFloat("Settings_Music Volume", 0.5f);
            PlayerPrefs.SetFloat("Settings_SFX Volume", .75f);

            //Camera
            PlayerPrefs.SetInt("Settings_CamMode", 0);
            PlayerPrefs.SetFloat("Settings_GeneralSens", 0.25f);
            PlayerPrefs.SetFloat("Settings_ScopeSens", 0.15f);


            #endregion

            PlayerPrefs.SetInt("FirstTime", 1);

            #region NetWorking
            PhotonNetwork.NickName = PlayerPrefs.GetString("UserStats_PlayerName");
            #endregion
        }
        else
        // data retrieving.
        {
            #region Recieve UserStats
            PlayerPrefs.SetString("UserStats_PlayerName", PlayerPrefs.GetString("UserStats_PlayerName"));

            LoadStats();

            #endregion

            PlayerPrefs.SetInt("FirstTime", PlayerPrefs.GetInt("FirstTime"));
        }

    }
    

 
    public void SaveStats()
    {
        ES3.Save("BgnCoins", BGNCoins);
        ES3.Save("CurrentLevel", CurrentLevel);
        ES3.Save("CurrentXP", CurrentXP);
        ES3.Save("Weaponinventory", Weaponinventory);
        ES3.Save("EquippedSkin", EquippedSkin);
        LoadStats();

    }

    public void LoadStats()
    {
        BGNCoins= ES3.Load("BgnCoins", BGNCoins);
        CoinsTextUI.text = ES3.Load("BgnCoins", BGNCoins).ToString();


        CurrentLevel = ES3.Load("CurrentLevel", CurrentLevel);
        CurrentLevelUI.text = ES3.Load("CurrentLevel", CurrentLevel).ToString();
        Weaponinventory = ES3.Load("Weaponinventory", Weaponinventory);

     EquippedSkin = ES3.Load<string>("EquippedSkin");


        #region load Inventory
        EquippedMelee = Weaponinventory.GetValueOrDefault("Melee");
        EquippedBackup = Weaponinventory.GetValueOrDefault("Backup");
        EquippedPrimary = Weaponinventory.GetValueOrDefault("Primary");
        EquippedHeavy = Weaponinventory.GetValueOrDefault("Heavy");
        #endregion



        #region XP
        CurrentXP = ES3.Load("CurrentXP", CurrentXP);
         ConvertedXP = CurrentXP.ToString() + "/" + (CurrentLevel * 99).ToString();
        if(CurrentXP > CurrentLevel*99)
        {
            CurrentLevel++;
            CurrentLevelUI.text = CurrentLevel.ToString();
            CurrentXP = 0;
            ConvertedXP = CurrentXP.ToString() + "/" + (CurrentLevel * 99).ToString();

        }

        ConvertedXPUi.text = ConvertedXP;
    
        #endregion


    }

}