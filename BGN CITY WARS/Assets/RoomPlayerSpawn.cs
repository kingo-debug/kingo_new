using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomPlayerSpawn : MonoBehaviour
{

    public Transform[] SpawnPoints;
    [SerializeField]
    private GameObject RoomScoreBoard;
    private GameObject SpawnedPlayer;
    // Start is called before the first frame update
    void Start()
        
    {
        Transform RandomSP = SpawnPoints[Random.Range(0,SpawnPoints.Length-1)];
       

        SpawnedPlayer = PhotonNetwork.Instantiate("BGN-Player new", RandomSP.position, Quaternion.identity);  // spawn player

        SpawnedPlayer.transform.GetChild(0).position = RandomSP.position; // set player position
      GameObject Scoreitem=   PhotonNetwork.Instantiate("_PLAYER SCORE BOARD ITEM_", transform.position, Quaternion.identity); // spawn player score board item

        Scoreitem.transform.parent = RoomScoreBoard.transform;
        Scoreitem.transform.localScale =new Vector3(1, 1, 1);
    }

}
