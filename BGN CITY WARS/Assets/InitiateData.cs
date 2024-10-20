using UnityEngine;
using Photon.Pun;




public class InitiateData : MonoBehaviour
{
    [SerializeField]
    private string DefaultSkinID = "SkinItem_322577"; // trev



    [Space(10)]
    [Header("initiate Inventory")]
    public string DefaultMelee;
    public string DefaultBackup;
    public string DefaultPrimary;
    public string DefaultHeavy;

    [Space(10)]
    [Header("initiate Default Vehicle")]
    public string DefaultVehicle;

    [Space(10)]
    [Header("initiate Armor")]
    public string DefaultArmor;

    private void Awake()
    {
    
        // data initiatation.
       if (ES3.Load<int>("FirstTime", 0) == 0)   //its like  public- int -FirstTime = 0
        {
             Debug.Log("Welcome FirstTimer");
              ES3.Save("FirstTime", 1);



            #region Player Stats FirstTime
            //SetUp New PlayerName
            int RandomNums = Random.Range(1000, 10000);
           string DefaultName = "Player " + RandomNums.ToString();
            ES3.Save("PlayerName", DefaultName);    //its like  public- int -FirstTime = 0 but dont need to give type  
            Debug.Log(DefaultName+" assigned as Player Name for this New User.");
            PhotonNetwork.NickName = DefaultName; // assign it to PhotonNetwork
                                                  // pLAYER Stats to default
            ES3.Save<int>("TotalScore", 0);
            ES3.Save<int>("TotalSandBoxWins", 0);
            ES3.Save<int>("TotalPVPWins", 0);
            ES3.Save<int>("TotalBodyShotKills",0); 
            ES3.Save<int>("TotalHeadShotKills", 0);
            ES3.Save<int>("TotalDeaths", 0);
            ES3.Save<string>("League", "Apex !");

            // Setup new Player XP and level
            int DefaultXP = 1;
            int DefaultLVL = 1;
            ES3.Save<int>("XP", DefaultXP);    //its like  public- int -FirstTime = 0 but dont need to give type  
            ES3.Save<int>("LVL", DefaultLVL);    //its like  public- int -FirstTime = 0 but dont need to give type  
            Debug.Log(DefaultXP + " Setup for new user XP");
            Debug.Log(DefaultLVL + " Setup for new user LVL");




            //Setup new Player  coins
            int DefaultBGNCoins = 10;
            ES3.Save<int>("BGNCoins", DefaultBGNCoins);

            //Setup new Player  Cash
            int DefaultBGNCash = 500;
            ES3.Save<int>("BGNCash", DefaultBGNCash);
            Debug.Log(DefaultBGNCash + "SetUp for new User Starting Cash");


            // Set Up new Player Default Skin        
            ES3.Save<string>("CurrentSkin", DefaultSkinID);
            Debug.Log(ES3.Load<string>("CurrentSkin") + "SetUp for new User as a default skin");

            #endregion

            #region Player Inventory FirstTime

            ES3.Save<string>("MeleeEquip", DefaultMelee);
            ES3.Save<string>("BackupEquip", DefaultBackup);
            ES3.Save<string>("PrimaryEquip", DefaultPrimary);
            ES3.Save<string>("HeavyEquip", DefaultHeavy);
            #endregion

            #region Player Armor FirstTime

            ES3.Save<string>("CurrentArmor", DefaultArmor);

            #endregion

            #region Player Vehicle FirstTime

            ES3.Save<string>("CurrentVehicle", DefaultVehicle);

            #endregion

            #region UserSettings First Time
            //audio
            ES3.Save<float>("Music",0.5f);
            ES3.Save<float>("SFX", 1f);
            //Sensetivity
            ES3.Save<float>("GeneralSense", 0.6f);
            ES3.Save<float>("ScopeSense", 0.6f);
            //CamMode
            ES3.Save<int>("CameraMode", 0);
            #endregion
        }
        else
        {
            Debug.Log("Welcome Back");


            #region Player Stats Load
            //Retrieve PlayerName
            int RandomNums = Random.Range(1000, 10000);
            Debug.Log(ES3.Load<string>("PlayerName")+ "  was Retrieved From Saved File as User's Name");
            PhotonNetwork.NickName = ES3.Load<string>("PlayerName"); // load it after file was modified like this


            // retrieve Player Xp and lvl

            Debug.Log(ES3.Load<int>("XP") + " was Retrieved From Saved File as Player XP");
            Debug.Log(ES3.Load<int>("LVL") + " was   Retrieved From Saved File asPlayer  LVL");



            //retrieve Coins
            Debug.Log(ES3.Load<int>("BGNCoins") + " was  Retrieved From Saved File for User BGN Coins");

            //retrieve Cash
            Debug.Log(ES3.Load<int>("BGNCash") + " was  Retrieved From Saved File for User BGN Cash");
            #endregion

            // retrieve skin   
            Debug.Log(ES3.Load<string>("CurrentSkin") + "  was Retrieved from file for default skin");
            // retrieve Equiped armor   
            Debug.Log(ES3.Load<string>("CurrentArmor") + "  was Retrieved from file for CurrentArmor");

        }
       
    }
    
 }