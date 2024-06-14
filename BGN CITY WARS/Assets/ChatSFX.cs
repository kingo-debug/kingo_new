using UnityEngine;
using UnityEngine.UI;
public class ChatSFX : MonoBehaviour
{

    public AudioClip ChatSound;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("ROOM DATA").transform.GetChild(1).transform.GetChild(3).GetComponent<Toggle>().isOn)
        {
            GetComponent<AudioSource>().PlayOneShot(ChatSound);
        }
        
    }



}
