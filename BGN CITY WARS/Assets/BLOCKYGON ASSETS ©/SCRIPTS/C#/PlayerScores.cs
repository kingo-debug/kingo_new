
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerScores : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private  PhotonView PV;
    [SerializeField]
    private TMPro.TextMeshProUGUI NickName;
    [SerializeField]
    private TMPro.TextMeshProUGUI KillCount;
    [SerializeField]
    private GameObject PlayerOwner;

    private void Start()
    {
        FindParentItem();
        PV = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(true);

            Invoke("FindParentItem", .15f);
        }
        Invoke("UpdateScoreData", 1f);
    }

    [Header("Player Room Status")]
    public string PlayerName;
    public int TotalRoomKills;
    public int TotalRoomDeaths;
    private int LastTotalKills;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // is Writing(CurrentPlayer)
        {
            stream.SendNext(TotalRoomKills);   //room kills Track
            stream.SendNext(PlayerName); // Player Name track
        

        }
        else if (stream.IsReading) // is Reading(otherplayers)
        {          
            TotalRoomKills = (int)stream.ReceiveNext(); // total room kills      
            PlayerName = (string)stream.ReceiveNext();
        }

    }




    void Awake()
    {
        gameObject.name = "0";    }



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
            transform.parent = GameObject.Find("SCENE Canvas").transform.GetChild(1).GetChild(1).GetChild(0);
            if(transform.localScale != new Vector3(1,1,1))
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
               
            
        
        }


    }





    public void UpdateScoreData()
    {

        GameObject Findobject = GameObject.Find(PV.Owner.ToString());
        if(Findobject !=null)
        {
            PlayerOwner= Findobject.gameObject.transform.GetChild(0).gameObject;  //find player owner object for local
            
        }

        if (PV.IsMine)
        {
        
            PlayerOwner.GetComponent<PlayerActionsVar>().ScoreItemUI = gameObject;

            NickName.text = PlayerOwner.GetComponent<PhotonSerializerBGN>().PlayerNickName;

            KillCount.text = PlayerOwner.GetComponent<PlayerActionsVar>().TotalRoomkillsTrack.ToString();

            gameObject.name = KillCount.text;

            TotalRoomKills = PlayerOwner.GetComponent<PlayerActionsVar>().TotalRoomkillsTrack;

            PlayerName = PhotonNetwork.NickName;

        }
        else
        {
       //   PlayerOwner = GameObject.Find(PV.Owner.ToString()).gameObject.transform.GetChild(0).gameObject;   //find player owner object for others

            NickName.text = PlayerOwner.GetComponent<PhotonSerializerBGN>().PlayerNickName;

            KillCount.text = TotalRoomKills.ToString();
            gameObject.name = KillCount.text;

        }
  


    }

    private void Update()
    {
        if(!PV.IsMine && TotalRoomKills!= LastTotalKills)
        {
         
            UpdateScoreData();
            KillCount.text = TotalRoomKills.ToString();
            LastTotalKills = TotalRoomKills;

        }
    }

}