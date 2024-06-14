using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class SpawnChatItem : MonoBehaviour
{
    [SerializeField]
   private  Transform ParentItem;
    [SerializeField]
    private Transform Inputfield;



    private PhotonView PV;
    private GameObject MessageItem;
    [SerializeField]
    private GameObject Messagesfx;

    private void Start()
    {
        PV = this.GetComponent<PhotonView>();
    }
    public void SpawnChat()
    {
        if (Inputfield.GetComponent<TMP_InputField>().text.Length > 0)
        {
            MessageItem = PhotonNetwork.Instantiate("MESSAGE ITEM_", transform.position, transform.rotation);
            MessageItem.transform.parent = ParentItem;
            #region SetTXTmessage
            {
                PV.RPC("SetTXTmessage", RpcTarget.AllBuffered);
            }
            #endregion
            #region Alert All Players
            if(Messagesfx.GetComponent<Toggle>().isOn)
            {
                // Find all GameObjects with the tag "Player"
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

                // Iterate through each player and get their PhotonView component
                foreach (GameObject player in players)
                {
                    PhotonView photonView = player.GetComponent<PhotonView>();

                    if (photonView != null)
                    {
                        photonView.RPC("PlayMessageNotification", RpcTarget.Others);
                    }
                    else
                    {
                        Debug.LogWarning("No PhotonView found on: " + player.name);
                    }
                }
            }
        
            #endregion

        }

        else  Debug.Log("MessageTooShort");

    }
    [PunRPC]
    public void SetTXTmessage()
    {
        MessageItem.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Inputfield.GetComponent<TMP_InputField>().text;  // set chat item logic
        MessageItem.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName + " : ";  // set chatter name item logic
    }

}
