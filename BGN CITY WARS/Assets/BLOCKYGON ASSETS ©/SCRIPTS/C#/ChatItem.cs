using UnityEngine;
using TMPro;
using Photon.Pun;

public class ChatItem : MonoBehaviour,IPunObservable
{
    [SerializeField]
    private TextMeshProUGUI txt;
    private  PhotonView PV;
    private  bool Set;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if(transform.parent==null)
        {
            transform.parent = GameObject.Find("ROOM DATA").transform.GetChild(1).transform.GetChild(6).transform.GetChild(0).transform.GetChild(0);
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
