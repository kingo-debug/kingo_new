using UnityEngine;
using Photon.Pun;
public class MainMapTargetCenter : MonoBehaviour
{
    private MainMapWindow mainmap;
    [SerializeField] Transform PlayerTarget;
    [SerializeField] private PhotonView PV;
    private void OnEnable()
    {
        if(PV.IsMine)
        {
            if (mainmap == null)
            {
                mainmap = GameObject.Find("SCENE MAPS").transform.GetChild(1).transform.GetChild(3).GetComponent<MainMapWindow>();
                mainmap.playerTarget = PlayerTarget;
            }
            else
            {
                mainmap.playerTarget = PlayerTarget;
            }

        }



    }

}
