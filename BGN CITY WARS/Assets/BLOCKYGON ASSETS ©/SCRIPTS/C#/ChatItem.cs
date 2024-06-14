using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ChatItem : MonoBehaviour,IPunObservable
{
    [SerializeField]
    private TextMeshProUGUI txt;
    PhotonView PV;
    bool Set;

    private void Start()
    {
        PV = this.GetComponent<PhotonView>();
        if(transform.parent==null)
        {
            transform.parent =  GameObject.Find("MESSAGE ITEMS").transform;
        }
        
     
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
     if (!Set)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(txt.text);
                Set = true;
            }
      else
            {
                txt.text = (string)stream.ReceiveNext(); 
            }
        }
    }
}
