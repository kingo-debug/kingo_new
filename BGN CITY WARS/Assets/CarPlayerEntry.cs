using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Car;

public class CarPlayerEntry : MonoBehaviour
{

    private PhotonView PlayerPV;
    private PhotonView PV;
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

    private ExplodeEventCaller explodeevent;
    [SerializeField]
    private GameObject EFX;


    private void Start()
    {
        carcontroller = GetComponent<CarController>();
        PV = GetComponent<PhotonView>();
        explodeevent = GetComponent<ExplodeEventCaller>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine)
        {
            Debug.Log(other.name + "InRange");
            if(other.CompareTag("Player")&& other.gameObject.GetComponent<PhotonView>().IsMine && ! explodeevent.Exploded)
            {
                Player = other.gameObject;
                DoorUIButton = Player.transform.Find("PLAYER Canvas").transform.Find("CF2-Rig").transform.GetChild(0).transform.Find("CarDoor").gameObject;
                DoorUIButton.SetActive(true);
                Player.GetComponent<CarSpawner>().CarinRange = gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + "OUTRange");
        if (other.CompareTag("Player"))
        {
            if (PV.IsMine &&! explodeevent.Exploded)
            {
                DoorUIButton.SetActive(false);
                Player.GetComponent<CarSpawner>().CarinRange = null;
            }
        }
    }


    [PunRPC]
    public void EnterCar()
    {
        if(PV.IsMine)
        {
            Player.transform.root.gameObject.SetActive(false); // Disable Player
            GetComponent<CarController>().enabled = true;
            GetComponent<CarUserControl>().enabled = true;
            GetComponent<CarAudio>().enabled = true;
            GetComponent<Animator>().enabled = true;
            if (TryGetComponent<AudioSource>(out AudioSource AS))
            {
                AS.enabled = true;
            }
                PlayerInCar = true;

            //Local Systems
            transform.parent.GetChild(1).gameObject.SetActive(true); // Car Cameras Enable
            transform.Find("CAR CANVAS").transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {   
            GetComponent<CarAudio>().enabled = true;
            GetComponent<AudioSource>().enabled = true;
            GetComponent<Animator>().enabled = true;
            PlayerInCar = true;
        }

    }
    [PunRPC]
    public void ExitCar()
    {
        if (PV.IsMine)
        {
        Player.transform.position = transform.Find("EXIT POINTS").transform.Find("LEFT").transform.position;
        transform.parent.GetChild(1).gameObject.SetActive(false); // Car Cameras Disable
        transform.Find("CAR CANVAS").transform.GetChild(0).gameObject.SetActive(false); // disable car canvas
        GetComponent<CarController>().enabled = false; // disable car controller
        GetComponent<CarUserControl>().enabled = false; //disable car controls
        Player.transform.root.gameObject.SetActive(true); // Enable Player
        carcontroller.Move(0, 0, 50000, 50000);
        GetComponent<AudioSource>().enabled = false;
            GetComponent<Animator>().enabled = false;
            Player.GetComponent<TakeDamage>().StartCoroutine("Checklife");
        PlayerInCar = false;
        }
        else
        {           
            GetComponent<AudioSource>().enabled = false;
            GetComponent<Animator>().enabled = false;
            PlayerInCar = false;
        }
    }
    private void Update()
    {
        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.E) && PlayerInCar)
        {
            ExitCar();
        }
        
    }
    public void ForceOutofRange()
    {
        if(Player!=null)
        {
            DoorUIButton.SetActive(false);
            Player.GetComponent<CarSpawner>().CarinRange = null;
        }
     
    }
}
