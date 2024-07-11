using UnityEngine;
using UnityStandardAssets.Vehicles.Car;


public class CrashDetectManager : MonoBehaviour
{
    private CarController carcontroller;

    [SerializeField]
    private AudioClip Crash1SFX;
    [SerializeField]
    private AudioClip Crash2SF;

    [SerializeField]
    private float MinSpeedCrash;

    private AudioSource AS;


    // Start is called before the first frame update
    void Start()
    {
        carcontroller = GetComponent<CarController>();
       AS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (carcontroller.Speed> MinSpeedCrash)
        {
            AS.PlayOneShot(Crash1SFX);
        }
       
    }
}
