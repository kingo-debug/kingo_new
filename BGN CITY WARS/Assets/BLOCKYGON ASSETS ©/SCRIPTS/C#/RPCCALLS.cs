using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RPCCALLS : MonoBehaviourPunCallbacks
{ 
    public GameObject AlertItem;


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject NewAlertItem = GameObject.Instantiate(AlertItem, gameObject.transform);

        NewAlertItem.GetComponent<TextMeshProUGUI>().text = newPlayer.NickName + (" Joined The Game");
        NewAlertItem.GetComponent<TextMeshProUGUI>().color = Color.green;
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        GameObject textnotif = GameObject.Instantiate(AlertItem, gameObject.transform);

        textnotif.GetComponent<TextMeshProUGUI>().text = newPlayer.NickName + (" Left The Game");
        textnotif.GetComponent<TextMeshProUGUI>().color = Color.red;
    }













}




