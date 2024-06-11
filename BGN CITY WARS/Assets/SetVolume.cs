using UnityEngine;

public class SetVolume : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string SoundData;

    void Start()
    {
       GetComponent<AudioSource>().volume = ES3.Load<float>(SoundData);
    }
}
