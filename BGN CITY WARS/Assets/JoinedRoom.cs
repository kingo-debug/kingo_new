using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JoinedRoom : MonoBehaviourPunCallbacks
{
    public string RoomSceneName;
    public string GameMode;
    public Transform LoadingScreen;
    [SerializeField]
    private int MaxroomsAllowed;
    [SerializeField]
    private byte MaxPlayersinRoom;
    [SerializeField]
    private Transform ErrorMaxRooms;

    [SerializeField]
    private Transform ErrorRoomFull;


    public override void OnJoinedRoom()
    {
        string RoomName = PhotonNetwork.CurrentRoom.Name;
        if(LoadingScreen!=null )
        {

            LoadingScreen.gameObject.SetActive(true);

        }

        
    }






    public void CreateOrJoinroom()
    {
        if (PhotonNetwork.CountOfRooms < MaxroomsAllowed)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MaxPlayersinRoom; // Set the maximum number of players for the room
            TypedLobby lobby = new TypedLobby(GameMode, LobbyType.Default);
            PhotonNetwork.JoinOrCreateRoom(RoomSceneName, roomOptions, lobby);
            PhotonNetwork.LoadLevel(RoomSceneName);
        }
        else
            ErrorMaxRooms.gameObject.SetActive(true);
    }
}
