using UnityEngine;
using Photon.Pun;
using TMPro;

public class UIOverHeadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI UINameDisplay;
    void Start()
    {
        Invoke("UpdateUINameDisplay",0.01f);
    }
    public void UpdateUINameDisplay()
    {
        UINameDisplay.text = ES3.Load<string>("PlayerName");
    }


}
