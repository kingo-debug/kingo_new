using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;

public class CarPlayerEntry : MonoBehaviour
{

    private PhotonView PlayerPV;
    [SerializeField]
    private GameObject DoorUIButton;
    public GameObject Player;

    [Header("SFX")]
    [SerializeField]
    private AudioClip EnterSound;
    [SerializeField]
    private AudioClip StartSound;
    private AudioSource AS;
    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "InRange");
        if(other.CompareTag("Player"))
        {
            PlayerPV = other.GetComponent<PhotonView>();
            Player = other.gameObject;
            if (PlayerPV.IsMine)
            {
                DoorUIButton = PlayerPV.gameObject.transform.Find("PLAYER Canvas").transform.Find("CF2-Rig").transform.GetChild(0).transform.Find("CarDoor").gameObject;
                DoorUIButton.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + "InRange");
        if (other.CompareTag("Player"))
        {
            PlayerPV = other.GetComponent<PhotonView>();
            if (PlayerPV.IsMine)
            {
                DoorUIButton.SetActive(false);
            }
        }
    }



    public void EnterCar()
    {
        Player.transform.root.gameObject.SetActive(false); // Disable Player
        transform.parent.GetChild(1).gameObject.SetActive(true); // Car Cameras Enable
        transform.Find("CAR CANVAS").gameObject.SetActive(true);
        GetComponent<CarController>().enabled = true;
        GetComponent <CarUserControl>().enabled = true;
        GetComponent<CarAudio>().enabled = true;
        AS.PlayOneShot(EnterSound);      AS.PlayOneShot(StartSound);

    }
}
