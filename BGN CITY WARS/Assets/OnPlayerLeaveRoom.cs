using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnPlayerLeaveRoom : MonoBehaviourPunCallbacks
{

    public override void OnPlayerLeftRoom(Player thePlayer)
    {
        Debug.Log(thePlayer.NickName +"Left Room");
        if (GetComponent<PhotonView>().Owner.NickName == thePlayer.NickName)          
        {
         //   PhotonNetwork.Destroy(gameObject);
        }
    }
}
