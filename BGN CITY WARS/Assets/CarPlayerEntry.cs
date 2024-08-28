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
    [SerializeField]
    private GameObject CarPointer;
    public GameObject Player;
    public bool PlayerInCar = false;
    [SerializeField]
    private UIBarRefresh HpUiBar;

    [Header("SFX")]
    [SerializeField]
    private AudioClip EnterSound;
    [SerializeField]
    private AudioClip StartSound;

    private ExplodeEventCaller explodeevent;
    [SerializeField]
    private GameObject EFX;

    [SerializeField]
    private LayerMask ExitPointCheck;

    [SerializeField]
    private GameObject[] OthersObjects;

    private void Start()
    {
        carcontroller = GetComponent<CarController>();
        PV = GetComponent<PhotonView>();
        explodeevent = GetComponent<ExplodeEventCaller>();
        if (!PV.IsMine)
        {
          //this.enabled = false;
        }


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
      //      GetComponent<CarController>().enabled = true;
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
            foreach (GameObject item in OthersObjects)
            {
                item.SetActive(true);
            }
        }
        CarPointer.SetActive(false);
        GetComponent<SetVolume>().RefreshVolume();
        HpUiBar.UpdateHP(GetComponent<TakeDamage>().HP);
    }
    [PunRPC]
    public void ExitCar()
    {
        if (PV.IsMine)
        {
            Transform exitPoints = transform.Find("EXIT POINTS");
            Transform leftExit = exitPoints.Find("LEFT");
            Transform rightExit = exitPoints.Find("RIGHT");
            Transform topExit = exitPoints.Find("TOP");

            Transform selectedExit = leftExit; // default to left

            if (Physics.Raycast(transform.position, -Vector3.right, 1f, ExitPointCheck))
            {
                if (Physics.Raycast(transform.position, Vector3.right, 1f, ExitPointCheck))
                {
                    selectedExit = topExit; // both left and right blocked, use top
                }
                else
                {
                    selectedExit = leftExit; // right blocked, use left
                }
            }
            else
            {
                selectedExit = rightExit; // right not blocked, use right
            }

            Player.transform.position = selectedExit.position;

            transform.parent.GetChild(1).gameObject.SetActive(false); // Car Cameras Disable
            transform.Find("CAR CANVAS").transform.GetChild(0).gameObject.SetActive(false); // disable car canvas
           // GetComponent<CarController>().enabled = false; // disable car controller
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
            foreach (GameObject item in OthersObjects)
            {
                item.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.E) && PlayerInCar)
        {
            // ExitCar();
            PV.RPC("ExitCar", RpcTarget.All);
          
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
