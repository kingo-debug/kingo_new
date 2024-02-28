using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UpdatePlayerCountInGame : MonoBehaviourPunCallbacks
{
    private TMPro.TextMeshProUGUI text;
 public void UpdatePlayerCount(int Players)
    {
        text.text = Players.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount);
    }
}

