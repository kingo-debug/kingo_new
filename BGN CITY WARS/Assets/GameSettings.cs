using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour
{
    
    [SerializeField]
    Slider GeneralSense;
    [SerializeField]
    Slider  ScopeSens;

    [SerializeField]
    Slider Music;
    [SerializeField]
    Slider SFX;


    void Start()
    {
    


        GeneralSense.value = ES3.Load<float>("GeneralSense");
        ScopeSens.value = ES3.Load<float>("ScopeSense");

        Music.value = ES3.Load<float>("Music");
        SFX.value = ES3.Load<float>("SFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGeneralSenseChange()
    {
       
       ES3.Save<float>("GeneralSense", GeneralSense.value);
        float convertedValue = ES3.Load<float>("GeneralSense") * 100;
        int integerValue = (int)convertedValue;

        GeneralSense.transform.GetChild(2).GetComponent<Text>().text = integerValue.ToString();

    }

    public void OnScopeSenseChange()
    {

        ES3.Save<float>("ScopeSense", ScopeSens.value);
        float convertedValue = ES3.Load<float>("ScopeSense") * 100;
        int integerValue = (int)convertedValue;

        ScopeSens.transform.GetChild(2).GetComponent<Text>().text = integerValue.ToString();

    }






    public void OnMusicValueChange()
    {

        ES3.Save<float>("Music", Music.value);
        float convertedValue = ES3.Load<float>("Music") * 100;
        int integerValue = (int)convertedValue;

        Music.transform.GetChild(2).GetComponent<Text>().text = integerValue.ToString();

    }

    public void OnSFXValueChange()
    {

        ES3.Save<float>("SFX", SFX.value);
        float convertedValue = ES3.Load<float>("SFX") * 100;
        int integerValue = (int)convertedValue;

        SFX.transform.GetChild(2).GetComponent<Text>().text = integerValue.ToString();

    }

}
