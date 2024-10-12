using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeagueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurentLeagueName;
    [SerializeField] private Image CurrentLeagueIcon;
    [SerializeField] private Image NexLeagueIcon;  // Next league icon
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
        ES3.Save<string>("League", CurrentLeagueIcon.sprite.name);
    }

    void UpdateLeague(int score)
    {
        // Define variables to hold the current league range
        int minScore = 0;
        int maxScore = 0;

        // Apex League: 20 point difference
        if (score >= 0 && score <= 20)
        {
            CurentLeagueName.text = "Apex 1";
            CurrentLeagueIcon.sprite = Apex1;
            NexLeagueIcon.sprite = Apex2;
            minScore = 0;
            maxScore = 20;
        }
        else if (score > 20 && score <= 40)
        {
            CurentLeagueName.text = "Apex 2";
            CurrentLeagueIcon.sprite = Apex2;
            NexLeagueIcon.sprite = Apex3;
            minScore = 20;
            maxScore = 40;
        }
        else if (score > 40 && score <= 60)
        {
            CurentLeagueName.text = "Apex 3";
            CurrentLeagueIcon.sprite = Apex3;
            NexLeagueIcon.sprite = ApexMaster;
            minScore = 40;
            maxScore = 60;
        }
        else if (score > 60 && score <= 80)
        {
            CurentLeagueName.text = "Apex Master";
            CurrentLeagueIcon.sprite = ApexMaster;
            NexLeagueIcon.sprite = Warrior1;
            minScore = 60;
            maxScore = 80;
        }

        // Warrior League: 40 point difference
        else if (score > 80 && score <= 120)
        {
            CurentLeagueName.text = "Warrior 1";
            CurrentLeagueIcon.sprite = Warrior1;
            NexLeagueIcon.sprite = Warrior2;
            minScore = 80;
            maxScore = 120;
        }
        else if (score > 120 && score <= 160)
        {
            CurentLeagueName.text = "Warrior 2";
            CurrentLeagueIcon.sprite = Warrior2;
            NexLeagueIcon.sprite = Warrior3;
            minScore = 120;
            maxScore = 160;
        }
        else if (score > 160 && score <= 200)
        {
            CurentLeagueName.text = "Warrior 3";
            CurrentLeagueIcon.sprite = Warrior3;
            NexLeagueIcon.sprite = WarriorMaster;
            minScore = 160;
            maxScore = 200;
        }
        else if (score > 200 && score <= 240)
        {
            CurentLeagueName.text = "Warrior Master";
            CurrentLeagueIcon.sprite = WarriorMaster;
            NexLeagueIcon.sprite = Reckless1;
            minScore = 200;
            maxScore = 240;
        }

        // Reckless League: 80 point difference
        else if (score > 240 && score <= 320)
        {
            CurentLeagueName.text = "Reckless 1";
            CurrentLeagueIcon.sprite = Reckless1;
            NexLeagueIcon.sprite = Reckless2;
            minScore = 240;
            maxScore = 320;
        }
        else if (score > 320 && score <= 400)
        {
            CurentLeagueName.text = "Reckless 2";
            CurrentLeagueIcon.sprite = Reckless2;
            NexLeagueIcon.sprite = Reckless3;
            minScore = 320;
            maxScore = 400;
        }
        else if (score > 400 && score <= 480)
        {
            CurentLeagueName.text = "Reckless 3";
            CurrentLeagueIcon.sprite = Reckless3;
            NexLeagueIcon.sprite = RecklessMaster;
            minScore = 400;
            maxScore = 480;
        }
        else if (score > 480 && score <= 560)
        {
            CurentLeagueName.text = "Reckless Master";
            CurrentLeagueIcon.sprite = RecklessMaster;
            NexLeagueIcon.sprite = Royal1;
            minScore = 480;
            maxScore = 560;
        }

        // Royal League: 160 point difference
        else if (score > 560 && score <= 720)
        {
            CurentLeagueName.text = "Royal 1";
            CurrentLeagueIcon.sprite = Royal1;
            NexLeagueIcon.sprite = Royal2;
            minScore = 560;
            maxScore = 720;
        }
        else if (score > 720 && score <= 880)
        {
            CurentLeagueName.text = "Royal 2";
            CurrentLeagueIcon.sprite = Royal2;
            NexLeagueIcon.sprite = Royal3;
            minScore = 720;
            maxScore = 880;
        }
        else if (score > 880 && score <= 1040)
        {
            CurentLeagueName.text = "Royal 3";
            CurrentLeagueIcon.sprite = Royal3;
            NexLeagueIcon.sprite = RoyalMaster;
            minScore = 880;
            maxScore = 1040;
        }
        else if (score > 1040)
        {
            CurentLeagueName.text = "Royal Master";
            CurrentLeagueIcon.sprite = RoyalMaster;
            NexLeagueIcon.sprite = null;  // No next league after Royal Master
            minScore = 1040;
            maxScore = 1200;
        }

        // Set max value of the slider to the league's range and reset the progress bar
        ProgressValue.maxValue = maxScore - minScore;
        ProgressValue.value = score - minScore;  // Set progress based on the current score within the league range
    }
}
