using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JoinedRoom : MonoBehaviourPunCallbacks
{
    public string SceneName;
    public string RoomName;
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

        if (LoadingScreen != null && PhotonNetwork.CurrentRoom.Name == RoomName)
        {

            LoadingScreen.gameObject.SetActive(true);
            PhotonNetwork.LoadLevel(SceneName);
        }

        
    }






    public void CreateOrJoinroom()
    {
        if (PhotonNetwork.CountOfRooms < MaxroomsAllowed)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MaxPlayersinRoom; // Set the maximum number of players for the room
            TypedLobby lobby = new TypedLobby(GameMode, LobbyType.Default);
            PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, lobby);

        }
        else
            ErrorMaxRooms.gameObject.SetActive(true);
    }
}
