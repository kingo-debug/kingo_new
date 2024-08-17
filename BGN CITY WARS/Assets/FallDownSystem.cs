using UnityEngine;
using Photon.Pun;
public class FallDownSystem : MonoBehaviour
{
    private Animator animator;
    private MainCharacterController mainCharacterController;
    private CharacterController characterController;
    private PhotonView PV;
    public bool Fell = false;
    private GameObject ScreenBlock;
    private MainCharacterController maincontroller;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        mainCharacterController = GetComponent<MainCharacterController>();
        characterController = GetComponent<CharacterController>();
        maincontroller = GetComponent<MainCharacterController>();
        ScreenBlock =transform.Find("PLAYER Canvas").transform.Find("SCREEN BLOCKERS").transform.GetChild(0).gameObject;
    }
    [PunRPC]
    public void FallDown()
    {
        
        animator.SetLayerWeight(3, 1);
        animator.SetBool("FellDown", true);
        #region Character Controller ReSize
        characterController.radius = 0;
        characterController.height = 0;
        characterController.center = new Vector3(0, .5f, 0);
        #endregion
        if(PV.IsMine)
        {
            mainCharacterController.StopAim();
            mainCharacterController.Combatmode = false;
            animator.SetBool("FIRE INPUT", false);
            ScreenBlock.SetActive(true);
            mainCharacterController.CanMove = false;
        }


        GetComponent<PlayerActionsVar>().canfire = false;

        Fell = true;


    }


    public void GotUp()
    {
        maincontroller.CanMove = true;
        animator.SetBool("FellDown", false);
            animator.SetLayerWeight(3, 0);
        #region Character Controller ReSize
        characterController.radius = 1;
        characterController.height = 4;
        characterController.center = new Vector3(0, 1.5f, 0);
        #endregion

        GetComponent<PlayerActionsVar>().canfire = true;

        Fell = false;
        if (PV.IsMine)
        {
            ScreenBlock.SetActive(false);
        }




    }

}
