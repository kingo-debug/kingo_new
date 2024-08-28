using UnityEngine;
using Photon.Pun;

public class CarSpawner : MonoBehaviour

{//SC
    [Header("Vehicle Entry")]
    public GameObject CarinRange;
    //Spawnvars
    [Space(10)]
    [Header("Vehicle Spawn Settings")]
    public Transform VehiclePos;
public GameObject VehicleToSpawn;
[SerializeField]
private GameObject VehichleSpawned;
 [Header("Player Spawn Settings")]
public Transform Player;
public Vector3 OffsetCheck;

//SpawnableVarslge

public float BlockRadius;

[SerializeField]
private bool Blocked;

//MessageVars
public GameObject MessageError;
public KeyCode Keybind;

public LayerMask layerMask;
    [Header("SpawnTime")]
    //Timer for respawn
 public float SpawnTime=1f;
[SerializeField]
private bool IsSpawned = false;
//level script access
[SerializeField]
private VehicleCoolDown vehicleCoolDown;

    private PhotonView pv;

    //set Player
    void Start()
    {
        Player = this.transform;
        vehicleCoolDown = GameObject.Find("VehicleCoolDown").GetComponent<VehicleCoolDown>();
        vehicleCoolDown.Player = this.gameObject;
        pv = GetComponent<PhotonView>();
       
    }



 void FixedUpdate() 
 {
MessageError.SetActive(Blocked);
CheckSpawnable();
CheckEntry();
}



    void CheckEntry()
    {
        if(CarinRange != null && ControlFreak2.CF2Input.GetKeyDown(KeyCode.T)&& pv.IsMine)
        {
            // CarinRange.GetComponent<CarPlayerEntry>().EnterCar();
            CarinRange.GetComponent<PhotonView>().RPC("EnterCar",RpcTarget.All);
        }
    }



    void CheckSpawnable()
{
    if (Physics.CheckSphere(Player.position + OffsetCheck + transform.forward,BlockRadius,layerMask))
        {
            Blocked = true;
        }

        else
        {
            Blocked = false;
        }

}


private void OnDrawGizmos()
{
    Gizmos.DrawWireSphere(Player.position + OffsetCheck + transform.forward,BlockRadius);
}



public void SpawnCar()
{
        if(vehicleCoolDown.Ready)
        {
            vehicleCoolDown.SpawnTimeValue = SpawnTime;
            if (IsSpawned)
            {
                PhotonNetwork.Destroy(VehichleSpawned);

                VehichleSpawned = PhotonNetwork.Instantiate(VehicleToSpawn.name, VehiclePos.position, VehiclePos.rotation);  // respawn car
                IsSpawned = true;
                vehicleCoolDown.SpawnTimeValue = SpawnTime;
                vehicleCoolDown.Ready = false;
                CarinRange.GetComponent<CarPlayerEntry>().ForceOutofRange();
            }

            else

            {
                VehichleSpawned = PhotonNetwork.Instantiate(VehicleToSpawn.name, VehiclePos.position, VehiclePos.rotation);  // spawn car first time
                IsSpawned = true;
                vehicleCoolDown.SpawnTimeValue = SpawnTime;
                vehicleCoolDown.Ready = false;
            }

        }
}


}//EC
