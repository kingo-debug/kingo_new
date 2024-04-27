using UnityEngine;
using Photon.Pun;
public class CarPlayerEntry : MonoBehaviour
{

    private PhotonView PlayerPV;
    [SerializeField]
    private GameObject DoorUIButton;
 void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "InRange");
        if(other.CompareTag("Player"))
        {
            PlayerPV = other.GetComponent<PhotonView>();
            if(PlayerPV.IsMine)
            {
                DoorUIButton = PlayerPV.gameObject.transform.Find("PLAYER Canvas").transform.Find("CF2-Rig").transform.Find("CarDoor").gameObject;
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
}
