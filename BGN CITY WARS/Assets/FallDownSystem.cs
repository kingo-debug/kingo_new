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
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        mainCharacterController = GetComponent<MainCharacterController>();
        characterController = GetComponent<CharacterController>();
        ScreenBlock =transform.Find("PLAYER Canvas").transform.Find("SCREEN BLOCKERS").transform.GetChild(0).gameObject;
    }
    [PunRPC]
    public void FallDown()
    {
        
        animator.SetLayerWeight(3, 1);
        animator.SetBool("FellDown", true);

        characterController.enabled = false;
        mainCharacterController.enabled = false;
        mainCharacterController.StopAim();
        mainCharacterController.Combatmode = false;
        animator.SetBool("FIRE INPUT", false);

        GetComponent<PlayerActionsVar>().canfire = false;

        Fell = true;

        ScreenBlock.SetActive(true);
    }


    public void GotUp()
    {
   
            animator.SetBool("FellDown", false);
            animator.SetLayerWeight(3, 0);
            characterController.enabled = true;
        GetComponent<PlayerActionsVar>().canfire = true;
        ScreenBlock.SetActive(false) ;
        Fell = false;
        if (PV.IsMine)
        {
            mainCharacterController.enabled = true;        
        }




    }

}
