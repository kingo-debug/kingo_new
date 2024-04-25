using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InRoomItemSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemToSpawm;

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject JPitem = PhotonNetwork.Instantiate(ItemToSpawm.name, transform.position, Quaternion.identity);
            JPitem.transform.parent = transform;
            JPitem.transform.localPosition = new Vector3(0, 0, 0);
        }
      

    }


}
