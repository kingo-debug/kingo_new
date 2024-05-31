using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;


public class PhotonVS : MonoBehaviourPunCallbacks
{
    public RoomItem RoomItemPrefab;
    List<RoomItem>roomitemlist = new List<RoomItem>();
    public Transform ContentObject;
   
public void SetUserName(string UserName)
{
PhotonNetwork.NickName = UserName;
}

 //   public override void OnRoomListUpdate(List<RoomInfo> roomList)
 //   {
      //  //base.OnRoomListUpdate(roomList);
      //  UpdateRoomList(roomList);
    //    Debug.Log("override");
    
 //   }
  void UpdateRoomList(List<RoomInfo> list)
{
    foreach (RoomItem item in roomitemlist)
    {
        Destroy(item.gameObject);
    }

    roomitemlist.Clear();
    Debug.Log("clear");

    foreach (RoomInfo room in list)
    {
        RoomItem newRoom = Instantiate(RoomItemPrefab, ContentObject);
        Debug.Log("instantiate");
        newRoom.SetRoomName(room.Name, room.PlayerCount, room.MaxPlayers);
        roomitemlist.Add(newRoom);
    }
}





//public void JoinRoom(string roomName)
//{
//PhotonNetwork.JoinRoom(roomName);
//}



   



}

