
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerScores : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private new PhotonView photonView;
    [SerializeField]
    private TMPro.TextMeshProUGUI NickName;
    [SerializeField]
    private TMPro.TextMeshProUGUI KillCount;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(true);

            Invoke("FindParentItem", .15f);
        }
        UpdateScoreData();
    }

    [Header("Player Room Status")]
    public string PlayerName;
    public int TotalRoomKills;
    public int TotalRoomDeaths;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // is Writing(CurrentPlayer)
        {
            Debug.Log("before stream.SendNext(PlayerName +  stream.SendNext(TotalRoomKills");
            stream.SendNext(PlayerName);
            stream.SendNext(TotalRoomKills);   //room kills Track
            Debug.Log("after stream.SendNext(PlayerName +  stream.SendNext(TotalRoomKills");
        }
        else if (stream.IsReading) // is Reading(otherplayers)
        {
            Debug.Log("before TotalRoomKills = (int)stream.ReceiveNext" + "PlayerName = (string)stream.ReceiveNext");
            PlayerName = (string)stream.ReceiveNext();
            TotalRoomKills = (int)stream.ReceiveNext(); // total room kills
            Debug.Log("TotalRoomKills = (int)stream.ReceiveNext" + "PlayerName = (string)stream.ReceiveNext");
        }

    }








    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateScoreData();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateScoreData();
    }



    private void FindParentItem()
    {
        if (transform.parent == null)
        {
            transform.parent = GameObject.Find("SCENE Canvas").transform.GetChild(0).GetChild(1).GetChild(0);
            Invoke("scale", 1);
            void scale()
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        
        }


    }







    public void UpdateScoreData()
    {

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (photonView.IsMine)
            {
                NickName.text = player.GetComponent<PhotonSerializerBGN>().PlayerNickName;
                gameObject.name = KillCount.text = player.GetComponent<PlayerActionsVar>().TotalRoomkillsTrack.ToString();
                
            }
            else
            {
                NickName.text = player.GetComponent<PhotonSerializerBGN>().PlayerNickName;
                gameObject.name = KillCount.text = TotalRoomKills.ToString();
             
            }

        }
    }

}