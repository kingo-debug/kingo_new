using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;

public class CarPlayerEntry : MonoBehaviour
{

    private PhotonView PlayerPV;
    private CarController carcontroller;
    [SerializeField]
    private GameObject DoorUIButton;
    public GameObject Player;
    public bool PlayerInCar = false;

    [Header("SFX")]
    [SerializeField]
    private AudioClip EnterSound;
    [SerializeField]
    private AudioClip StartSound;

    [SerializeField]
    private GameObject EFX;


    private void Start()
    {
        carcontroller = GetComponent<CarController>();
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
                Player.GetComponent<CarSpawner>().CarinRange = gameObject;
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
                Player.GetComponent<CarSpawner>().CarinRange = null;
            }
        }
    }



    public void EnterCar()
    {
        Player.transform.root.gameObject.SetActive(false); // Disable Player
        transform.parent.GetChild(1).gameObject.SetActive(true); // Car Cameras Enable
        transform.Find("CAR CANVAS").transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<CarController>().enabled = true;
        GetComponent <CarUserControl>().enabled = true;
        GetComponent<CarAudio>().enabled = true;
        GetComponent<AudioSource>().enabled = true;
        PlayerInCar = true;
}

    public void ExitCar()
    {
        Player.transform.position = transform.Find("EXIT POINTS").transform.Find("LEFT").transform.position;
        transform.parent.GetChild(1).gameObject.SetActive(false); // Car Cameras Disable
        transform.Find("CAR CANVAS").transform.GetChild(0).gameObject.SetActive(false); // disable car canvas
        GetComponent<CarController>().enabled = false; // disable car controller
        GetComponent<CarUserControl>().enabled = false; //disable car controls
        Player.transform.root.gameObject.SetActive(true); // Enable Player
        carcontroller.Move(0, 0, 50000, 50000);

        GetComponent<AudioSource>().enabled = false;

        PlayerInCar = false;

    }
    private void Update()
    {
        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.E) && PlayerInCar)
        {
            ExitCar();
        }
        
    }

}
