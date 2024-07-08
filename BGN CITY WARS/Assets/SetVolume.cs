using UnityEngine;

public class SetVolume : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string SoundData;
    private AudioSource AS;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        RefreshVolume();
    }

    private void OnEnable()
    {
        Invoke("RefreshVolume", 0.01f);
    }


    public void RefreshVolume()
    {
        AS.volume = ES3.Load<float>(SoundData);
    }
}
