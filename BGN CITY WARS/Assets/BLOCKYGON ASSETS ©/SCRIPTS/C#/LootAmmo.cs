
using UnityEngine;
using Photon.Pun;

public class LootAmmo : MonoBehaviour, IPunObservable
{
    private AudioSource AS;
    [SerializeField]
    private AudioClip PickupSFX;
    private GameObject Player;
    public bool Spawned = false;
    public bool PickedUp = false;
    private PhotonView PV;
    public float RespawnTime = 60.0f; // Set your countdown time in seconds here
    private float currentTime;
    private float previousSeconds = -1;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && PV != null)
        {
            stream.SendNext(currentTime); //Accelerating? this player
            stream.SendNext(Spawned); //Spawned loot ? this player
            stream.SendNext(PickedUp); //PickedUp loot ? this player
        }

        else
        {
            currentTime = (float)stream.ReceiveNext(); // other player loot currentTime?
            Spawned = (bool)stream.ReceiveNext(); // other player  loot Spawned?
            PickedUp = (bool)stream.ReceiveNext(); // other player  loot PickedUp?
        }
    }

    void Start()
    {
        AS = GetComponent<AudioSource>();

        Invoke("FindPlayer", 0.25f);
        PV = GetComponent<PhotonView>();
        currentTime = RespawnTime;
        if (PhotonNetwork.IsMasterClient)
        {
            RespawnLoot();
        }

    }

    private void Update()
    {
        if (!Spawned && PhotonNetwork.IsMasterClient)
        {
            currentTime -= Time.deltaTime;

            float seconds = Mathf.RoundToInt(currentTime % 60);

            if (seconds != previousSeconds)
            {
                float minutes = Mathf.Floor(currentTime / 60);
                previousSeconds = seconds;
            }


        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            if (Spawned)
            {
                RespawnLoot();
            }
            else
            {
                GetComponent<BoxCollider>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (currentTime <= 0) // check to spawn
        {
            RespawnLoot();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject;
            if (Player == other.gameObject)
            {
                PV.RPC("PickUP", RpcTarget.AllBufferedViaServer);
            }
        }
    }
    [PunRPC]
    void PickUP()
    {
        AS.PlayOneShot(PickupSFX);
        if (Player != null)
        {
            // Player.GetPhotonView().RPC("RestoreJetpackFuel", RpcTarget.AllBufferedViaServer);
            Player.GetComponent<WeaponStatus>().CurrentWeapon.GetComponent<WeaponShoot>().AmmoRefil();
        }
        PickedUp = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Spawned = false;
        currentTime = RespawnTime;



    }

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
       // if (GameObject.FindWithTag("Player").GetComponent<PhotonView>().IsMine)
        {
            //    PV = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
        }
    }
}
