using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeagueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurentLeagueName;
    [SerializeField] private Image CurrentLeagueIcon;
    [SerializeField] private Image NexLeagueIcon;
    [SerializeField] private Slider ProgressValue;
    [SerializeField] private TextMeshProUGUI TotalScore;
    private int TotalScores;

    [Space(10)]
    [Header("League List")]
    [SerializeField] private Sprite Apex1;
    [SerializeField] private Sprite Apex2;
    [SerializeField] private Sprite Apex3;
    [SerializeField] private Sprite ApexMaster;

    [SerializeField] private Sprite Warrior1;
    [SerializeField] private Sprite Warrior2;
    [SerializeField] private Sprite Warrior3;
    [SerializeField] private Sprite WarriorMaster;

    [SerializeField] private Sprite Reckless1;
    [SerializeField] private Sprite Reckless2;
    [SerializeField] private Sprite Reckless3;
    [SerializeField] private Sprite RecklessMaster;

    [SerializeField] private Sprite Royal1;
    [SerializeField] private Sprite Royal2;
    [SerializeField] private Sprite Royal3;
    [SerializeField] private Sprite RoyalMaster;

    void Start()
    {
        // Total Score is loaded and initially set
        TotalScores = ES3.Load<int>("TotalScore", 0);
        TotalScore.text = TotalScores.ToString();

        // Set Progress Value
        ProgressValue.value = TotalScores;

        // Initialize league settings
        UpdateLeague(TotalScores);
    }

    void OnEnable()
    {
        // Refresh the score and league info when the object becomes active
        TotalScores = ES3.Load<int>("TotalScore", 0);
        TotalScore.text = TotalScores.ToString();
        ProgressValue.value = TotalScores;

        // Update the league based on the loaded score
        UpdateLeague(TotalScores);
    }

    void UpdateLeague(int score)
    {
        // Check score ranges and update league name, icon, and slider max value
        if (score >= 0 && score <= 20)
        {
            CurentLeagueName.text = "Apex 1";
            CurrentLeagueIcon.sprite = Apex1;
            ProgressValue.maxValue = 20;
        }
        else if (score > 20 && score <= 40)
        {
            CurentLeagueName.text = "Apex 2";
            CurrentLeagueIcon.sprite = Apex2;
            ProgressValue.maxValue = 40;
        }
        else if (score > 40 && score <= 60)
        {
            CurentLeagueName.text = "Apex 3";
            CurrentLeagueIcon.sprite = Apex3;
            ProgressValue.maxValue = 60;
        }
        else if (score > 60 && score <= 80)
        {
            CurentLeagueName.text = "Apex Master";
            CurrentLeagueIcon.sprite = ApexMaster;
            ProgressValue.maxValue = 80;
        }
        else if (score > 80 && score <= 100)
        {
            CurentLeagueName.text = "Warrior 1";
            CurrentLeagueIcon.sprite = Warrior1;
            ProgressValue.maxValue = 100;
        }
        else if (score > 100 && score <= 120)
        {
            CurentLeagueName.text = "Warrior 2";
            CurrentLeagueIcon.sprite = Warrior2;
            ProgressValue.maxValue = 120;
        }
        else if (score > 120 && score <= 140)
        {
            CurentLeagueName.text = "Warrior 3";
            CurrentLeagueIcon.sprite = Warrior3;
            ProgressValue.maxValue = 140;
        }
        else if (score > 140 && score <= 160)
        {
            CurentLeagueName.text = "Warrior Master";
            CurrentLeagueIcon.sprite = WarriorMaster;
            ProgressValue.maxValue = 160;
        }
        else if (score > 160 && score <= 180)
        {
            CurentLeagueName.text = "Reckless 1";
            CurrentLeagueIcon.sprite = Reckless1;
            ProgressValue.maxValue = 180;
        }
        else if (score > 180 && score <= 200)
        {
            CurentLeagueName.text = "Reckless 2";
            CurrentLeagueIcon.sprite = Reckless2;
            ProgressValue.maxValue = 200;
        }
        else if (score > 200 && score <= 220)
        {
            CurentLeagueName.text = "Reckless 3";
            CurrentLeagueIcon.sprite = Reckless3;
            ProgressValue.maxValue = 220;
        }
        else if (score > 220 && score <= 240)
        {
            CurentLeagueName.text = "Reckless Master";
            CurrentLeagueIcon.sprite = RecklessMaster;
            ProgressValue.maxValue = 240;
        }
        else if (score > 240 && score <= 260)
        {
            CurentLeagueName.text = "Royal 1";
            CurrentLeagueIcon.sprite = Royal1;
            ProgressValue.maxValue = 260;
        }
        else if (score > 260 && score <= 280)
        {
            CurentLeagueName.text = "Royal 2";
            CurrentLeagueIcon.sprite = Royal2;
            ProgressValue.maxValue = 280;
        }
        else if (score > 280 && score <= 300)
        {
            CurentLeagueName.text = "Royal 3";
            CurrentLeagueIcon.sprite = Royal3;
            ProgressValue.maxValue = 300;
        }
        else if (score > 300)
        {
            CurentLeagueName.text = "Royal Master";
            CurrentLeagueIcon.sprite = RoyalMaster;
            ProgressValue.maxValue = 320;
        }
    }
}
