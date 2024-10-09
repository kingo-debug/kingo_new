using UnityEngine;
using TMPro;
using Photon.Pun;
public class GameWinner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private RoomGameManager GameManager;
    [SerializeField] private TextMeshProUGUI TextNotice;
    [SerializeField] private GameObject WinnerCrowns;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Invoke("CheckWinner", 0.05f);
    }

    void CheckWinner()
    {
        if (GameManager.CurrentWinnner== null) //draw
        {
            TextNotice.text = "ROUND OVER";
            WinnerCrowns.SetActive(false);
        }
       else
        {
            TextNotice.text = GameManager.CurrentWinnner.GetComponent<PlayerScores>().PlayerName + "HOLDS CONTROL IN " + PhotonNetwork.CurrentRoom.Name; // announce a winner

            if(GameManager.CurrentWinnner.GetComponent<PhotonView>().IsMine) // check if Local player is the Winner
            {
                // reward !!
                WinnerCrowns.SetActive(true);
                ES3.Save<int>("TotalScore", ES3.Load<int>("TotalScore") + 1);
                ES3.Save<int>("TotalSandBoxWins", ES3.Load<int>("TotalSandBoxWins") + 1);
            }
            else
            {
                WinnerCrowns.SetActive(false);
            }
        }
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
