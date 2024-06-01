using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomPlayerSpawn : MonoBehaviour
{

    public Transform[] SpawnPoints;
    [SerializeField]
    private GameObject RoomScoreBoard;
    // Start is called before the first frame update
    void Awake()
        
    {
        Transform RandomSP = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        PhotonNetwork.Instantiate("BGN-Player new", RandomSP.position, Quaternion.identity);
      GameObject Scoreitem=   PhotonNetwork.Instantiate("_PLAYER SCORE BOARD ITEM_", transform.position, Quaternion.identity);
        Scoreitem.transform.localScale =new Vector3(1, 1, 1);
        Scoreitem.transform.parent = RoomScoreBoard.transform;
    }

}
