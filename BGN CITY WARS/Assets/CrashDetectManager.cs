using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using Photon.Pun;

public class CrashDetectManager : MonoBehaviour
{
    private CarController carcontroller;

    [SerializeField]
    private AudioClip Crash1SFX;
    [SerializeField]
    private AudioClip Crash2SF;

    [SerializeField]
    private float MinSpeedCrash;
    [SerializeField]
    private float MinSpeedRunOver = 5f;
    [SerializeField]
    private float CrashDamageAdjust = 3f;
    [SerializeField]
    private float PlayersDamageAdjust = 1.5f;
    private AudioSource AS;
    private TakeDamage takedamage;


    // Start is called before the first frame update
    void Start()
    {
        carcontroller = GetComponent<CarController>();
       AS = transform.Find("ETC").Find("Audio Source").gameObject.GetComponent<AudioSource>();
        takedamage = GetComponent<TakeDamage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")&& carcontroller.smoothedSpeed> MinSpeedCrash)
        {
            AS.PlayOneShot(Crash1SFX);  // damage car enviremental crash
            takedamage.Takedamage(Mathf.RoundToInt(carcontroller.smoothedSpeed/ CrashDamageAdjust));
        }

        else if (other.name ==("Player Root") && carcontroller.smoothedSpeed > MinSpeedRunOver) 
            // Run over players
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<PhotonView>().RPC("FallDown", RpcTarget.Others); // trip other player
            other.GetComponent<PhotonView>().RPC("Takedamage", RpcTarget.Others, Mathf.RoundToInt(carcontroller.smoothedSpeed / PlayersDamageAdjust)); // damage other player
        }

    }
}
