using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CheckPlayersInRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject NoPlayersUI;
    void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            DisplayNoPlayers(true);
        }
        else
        {
            DisplayNoPlayers(false);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount <2)
        {
            DisplayNoPlayers(true);
        }
        else
        {
            DisplayNoPlayers(false);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            DisplayNoPlayers(true);
        }
        else
        {
            DisplayNoPlayers(false);
        }
    }

    void DisplayNoPlayers(bool Check)
    {
        NoPlayersUI.SetActive(Check);
    }
}
