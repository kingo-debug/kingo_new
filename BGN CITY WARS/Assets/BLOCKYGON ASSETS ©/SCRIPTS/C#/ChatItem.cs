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

        #region Flip if Sender is Player
        if(PV.IsMine)
        {
            transform.GetChild(0).localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); // invert BGs
            transform.GetChild(0).transform.Find("chat text").localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
       //     transform.GetChild(1).gameObject.SetActive(false); // disable name
            transform.GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(850f, transform.position.y, transform.position.z);
        }
        #endregion



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
