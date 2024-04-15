using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackLoot : MonoBehaviour
{

private AudioSource AS;
    [SerializeField]
    private AudioClip PickupSFX;
    private GameObject Player;

    [SerializeField]
    float RespawnTime = 60f;
    public bool PickedUp = false;


    void Start()
    {
        AS = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player= other.gameObject ;
            PickUP();
        }
    }

    void PickUP()
    {
        AS.PlayOneShot(PickupSFX);
        Player.GetComponent<JetPackManager>().RestoreJetpackFuel();
        PickedUp = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke("RespawnLoot", RespawnTime);

    }

    void RespawnLoot()
    {
        PickedUp = false;
        GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
