using UnityEngine;
using Photon.Pun;
public class DieEfX : MonoBehaviour

{
 
 [SerializeField]
 private GameObject[] ObjectsToHide;
    private CharacterController characterController;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        characterController = GetComponent<CharacterController>();
    }
    public void HideOut()
    {
        if (!PV.IsMine)
        {
            foreach (GameObject item in ObjectsToHide)
        {
            item.gameObject.SetActive(false);
        }
        }

    }
    public void UnHide()
    {
        if (!PV.IsMine)
    {
            foreach (GameObject item in ObjectsToHide)
        {
            item.gameObject.SetActive(true);
        }
    }
   }
    void OnDied()
    {
        #region Character Controller ReSize
        characterController.radius = 0;
        characterController.height = 0;
        characterController.center = new Vector3(0, .5f, 0);
        #endregion
    }

}
