using UnityEngine;
using Photon.Pun;
using TMPro;
public class InitiateData : MonoBehaviour
{
    private bool NewPlayer = true;
    public int BGNCoins;
    public int CurrentXP;
    public int CurrentLevel;
    public string ConvertedXP;

    public Material Skin;
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
    void Start()
    {
        // data initiatation.
      if (PlayerPrefs.GetInt ("FirstTime") ==0)
        {
            int RandomNums = Random.Range(1000,10000);

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
    
            PlayerPrefs.SetInt("FirstTime",1);

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
        LoadStats();

    }

    public void LoadStats()
    {
        BGNCoins= ES3.Load("BgnCoins", BGNCoins);
        CoinsTextUI.text = ES3.Load("BgnCoins", BGNCoins).ToString();

        CurrentLevel = ES3.Load("CurrentLevel", CurrentLevel);
        CurrentLevelUI.text = ES3.Load("CurrentLevel", CurrentLevel).ToString();

        #region XP
        CurrentXP = ES3.Load("CurrentXP", CurrentXP);
         ConvertedXP = CurrentXP.ToString() + "/" + (CurrentLevel * 99).ToString();
        if(CurrentXP > CurrentLevel*99)
        {
            CurrentLevel++;
            CurrentLevelUI.text = ES3.Load("CurrentLevel", CurrentLevel).ToString();
            CurrentXP = 0;
            ConvertedXP = CurrentXP.ToString() + "/" + (CurrentLevel * 99).ToString();

        }

        ConvertedXPUi.text = ConvertedXP;
    
        #endregion


    }

}