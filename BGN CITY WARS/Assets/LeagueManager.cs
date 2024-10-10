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
        ES3.Save<Sprite>("LeagueLogo", CurrentLeagueIcon.sprite);
        // Initialize league settings
        UpdateLeague(TotalScores);
    }

    void OnEnable()
    {
        // Refresh the score and league info when the object becomes active
        TotalScores = ES3.Load<int>("TotalScore", 0);
        TotalScore.text = TotalScores.ToString();

        // Update the league based on the loaded score
        UpdateLeague(TotalScores);
        ES3.Save<Sprite>("LeagueLogo", CurrentLeagueIcon.sprite);
    }

    void UpdateLeague(int score)
    {
        // Define variables to hold the current league range
        int minScore = 0;
        int maxScore = 0;

        // Check score ranges and update league name, icon, slider max value, and reset progress bar to 0
        if (score >= 0 && score <= 20)
        {
            CurentLeagueName.text = "Apex 1";
            CurrentLeagueIcon.sprite = Apex1;
            minScore = 0;
            maxScore = 20;
        }
        else if (score > 20 && score <= 40)
        {
            CurentLeagueName.text = "Apex 2";
            CurrentLeagueIcon.sprite = Apex2;
            minScore = 20;
            maxScore = 40;
        }
        else if (score > 40 && score <= 60)
        {
            CurentLeagueName.text = "Apex 3";
            CurrentLeagueIcon.sprite = Apex3;
            minScore = 40;
            maxScore = 60;
        }
        else if (score > 60 && score <= 80)
        {
            CurentLeagueName.text = "Apex Master";
            CurrentLeagueIcon.sprite = ApexMaster;
            minScore = 60;
            maxScore = 80;
        }
        else if (score > 80 && score <= 100)
        {
            CurentLeagueName.text = "Warrior 1";
            CurrentLeagueIcon.sprite = Warrior1;
            minScore = 80;
            maxScore = 100;
        }
        else if (score > 100 && score <= 120)
        {
            CurentLeagueName.text = "Warrior 2";
            CurrentLeagueIcon.sprite = Warrior2;
            minScore = 100;
            maxScore = 120;
        }
        else if (score > 120 && score <= 140)
        {
            CurentLeagueName.text = "Warrior 3";
            CurrentLeagueIcon.sprite = Warrior3;
            minScore = 120;
            maxScore = 140;
        }
        else if (score > 140 && score <= 160)
        {
            CurentLeagueName.text = "Warrior Master";
            CurrentLeagueIcon.sprite = WarriorMaster;
            minScore = 140;
            maxScore = 160;
        }
        else if (score > 160 && score <= 180)
        {
            CurentLeagueName.text = "Reckless 1";
            CurrentLeagueIcon.sprite = Reckless1;
            minScore = 160;
            maxScore = 180;
        }
        else if (score > 180 && score <= 200)
        {
            CurentLeagueName.text = "Reckless 2";
            CurrentLeagueIcon.sprite = Reckless2;
            minScore = 180;
            maxScore = 200;
        }
        else if (score > 200 && score <= 220)
        {
            CurentLeagueName.text = "Reckless 3";
            CurrentLeagueIcon.sprite = Reckless3;
            minScore = 200;
            maxScore = 220;
        }
        else if (score > 220 && score <= 240)
        {
            CurentLeagueName.text = "Reckless Master";
            CurrentLeagueIcon.sprite = RecklessMaster;
            minScore = 220;
            maxScore = 240;
        }
        else if (score > 240 && score <= 260)
        {
            CurentLeagueName.text = "Royal 1";
            CurrentLeagueIcon.sprite = Royal1;
            minScore = 240;
            maxScore = 260;
        }
        else if (score > 260 && score <= 280)
        {
            CurentLeagueName.text = "Royal 2";
            CurrentLeagueIcon.sprite = Royal2;
            minScore = 260;
            maxScore = 280;
        }
        else if (score > 280 && score <= 300)
        {
            CurentLeagueName.text = "Royal 3";
            CurrentLeagueIcon.sprite = Royal3;
            minScore = 280;
            maxScore = 300;
        }
        else if (score > 300)
        {
            CurentLeagueName.text = "Royal Master";
            CurrentLeagueIcon.sprite = RoyalMaster;
            minScore = 300;
            maxScore = 320;
        }

        // Set max value of the slider to the league's range and reset the progress bar
        ProgressValue.maxValue = maxScore - minScore;
        ProgressValue.value = score - minScore;  // Set progress based on the current score within the league range
    }
}
