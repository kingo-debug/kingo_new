using UnityEngine;
using UnityEngine.UI;
public class UpdateLeagueIcon : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        GetComponent<Image>().sprite = ES3.Load<Sprite>("LeagueLogo");
    }

    
}
