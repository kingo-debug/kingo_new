using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusLoad : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentXP;
    public int CurrentLevel;
    public string ConvertedXP;

    [SerializeField]
    private TextMeshProUGUI CurrentLevelUI;

    [SerializeField]
    private TextMeshProUGUI ConvertedXPUi;

    [SerializeField]
    private TextMeshProUGUI CoinsTextUI;

    [SerializeField]
    private TextMeshProUGUI CashTextUI;
    void Start()
    {
        LoadStatus();
    }
    public void LoadStatus()
    {
          CurrentXP = ES3.Load<int>("XP");
          ConvertedXP = CurrentXP.ToString() + "/" + (CurrentLevel * 99).ToString();
           if(CurrentXP > CurrentLevel*99)
        {
            CurrentLevel++;
              CurrentLevelUI.text = CurrentLevel.ToString();
            CurrentXP = 0;
              ConvertedXP = CurrentXP.ToString() + "/" + (CurrentLevel * 99).ToString();

              }

           ConvertedXPUi.text = ConvertedXP;

        CurrentLevel = ES3.Load<int>("LVL");
        CurrentLevelUI.text = ES3.Load<int>("LVL").ToString();


        // coins load to UI text  from file

        CoinsTextUI.text = ES3.Load<int>("BGNCoins").ToString();

        // load cash

        CashTextUI.text = ES3.Load<int>("BGNCash").ToString();


    }


}
