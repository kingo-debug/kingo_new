using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JetPackLoot : MonoBehaviour
{

private AudioSource AS;
    [SerializeField]
    private AudioClip PickupSFX;
    private GameObject Player;

    [SerializeField]
    float RespawnTime = 60f;
    public bool PickedUp = false;
    private PhotonView PV;
  


    void Start()
    {
        AS = GetComponent<AudioSource>();

        Invoke("FindPlayer", 0.25f);
        PV = GetComponent<PhotonView>();


    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player= other.gameObject ;
        PV.RPC("PickUP", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    void PickUP()
    {
        AS.PlayOneShot(PickupSFX);
        Player.GetComponent<JetPackManager>().RestoreJetpackFuel();
        PickedUp = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("RespawnLoot", RespawnTime);

    }
   [PunRPC]
    void RespawnLoot()
    {
        PickedUp = false;
        GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void FindPlayer()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PhotonView>().IsMine)
        {
        //    PV = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
        }
    }
}
