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
    public bool Spawned = true;
    public bool PickedUp = false;
    private PhotonView PV;
    public float RespawnTime = 60.0f; // Set your countdown time in seconds here
    private float currentTime;
    private float previousSeconds = -1;
    


    void Start()
    {
        AS = GetComponent<AudioSource>();

        Invoke("FindPlayer", 0.25f);
        PV = GetComponent<PhotonView>();
        currentTime = RespawnTime;
        Spawned = true;
    }

    private void Update()
    {
        if(!Spawned)
        {
            currentTime -= Time.deltaTime;

            float seconds = Mathf.RoundToInt(currentTime % 60);

            if (seconds != previousSeconds)
            {
                float minutes = Mathf.Floor(currentTime / 60);
                previousSeconds = seconds;
            }

            if (currentTime <= 0)
            {
                PV.RPC("RespawnLoot", RpcTarget.AllBufferedViaServer);
            }
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player= other.gameObject ;
            if(Player==other.gameObject)
            {
                PV.RPC("PickUP", RpcTarget.AllBufferedViaServer);
            }
         
        }
    }
    [PunRPC]
    void PickUP()
    {
        AS.PlayOneShot(PickupSFX);  
        if(Player!=null)
        {
           // Player.GetComponent<JetPackManager>().RestoreJetpackFuel();
            Player.GetPhotonView().RPC("RestoreJetpackFuel", RpcTarget.AllBufferedViaServer);
        }    
        PickedUp = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Spawned = false;
        currentTime = RespawnTime;



    }
   [PunRPC]
    void RespawnLoot()
    {
        PickedUp = false;
        GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        currentTime = RespawnTime;
        Spawned = true;


    }
    void FindPlayer()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PhotonView>().IsMine)
        {
        //    PV = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
        }
    }
}
