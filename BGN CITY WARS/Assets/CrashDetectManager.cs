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
    private float CrashDamageAdjust = 3f;
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
            takedamage.Takedamage(Mathf.RoundToInt(carcontroller.smoothedSpeed/2));
        }

        else if (other.CompareTag("Player") && carcontroller.smoothedSpeed > MinSpeedCrash) 
            // Run over players
        {
            other.GetComponent<PhotonView>().RPC("FallDown", RpcTarget.Others);
            other.GetComponent<TakeDamage>().Takedamage(Mathf.RoundToInt(carcontroller.smoothedSpeed / 2));
        }

    }
}
