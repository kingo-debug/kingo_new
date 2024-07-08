using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour
{
    
    [SerializeField]
    Slider GeneralSense;
    [SerializeField]
    Slider  ScopeSens;

    [SerializeField]
    Slider MusicSlider;
    [SerializeField]
    Slider SFXSlider;
    [SerializeField]
    private AudioSource MusicAS;
    [SerializeField]
    private AudioSource SFXAS;
    void Start()
    {
        MusicAS = GameObject.Find("BG MUSIC").GetComponent<AudioSource>();
        SFXAS = GameObject.Find("SFX").GetComponent<AudioSource>();
        GeneralSense.value = ES3.Load<float>("GeneralSense");
        ScopeSens.value = ES3.Load<float>("ScopeSense");

        MusicSlider.value = ES3.Load<float>("Music");
        SFXSlider.value = ES3.Load<float>("SFX");

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

        ES3.Save<float>("Music", MusicSlider.value);
        float convertedValue = ES3.Load<float>("Music") * 100;
        int integerValue = (int)convertedValue;

        MusicSlider.transform.GetChild(2).GetComponent<Text>().text = integerValue.ToString();

        MusicAS.volume = ES3.Load<float>("Music"); // update Music volume



    }

    public void OnSFXValueChange()
    {

        ES3.Save<float>("SFX", SFXSlider.value);
        float convertedValue = ES3.Load<float>("SFX") * 100;
        int integerValue = (int)convertedValue;

        SFXSlider.transform.GetChild(2).GetComponent<Text>().text = integerValue.ToString();

        SFXAS.volume = ES3.Load<float>("SFX"); // update SFX volume

        // Find all objects with the SetVolume component and call RefreshVolume on each
        SetVolume[] objectsWithSetVolume = FindObjectsOfType<SetVolume>();
        foreach (SetVolume setVolume in objectsWithSetVolume)
        {
            setVolume.RefreshVolume();
        }
    }

}
