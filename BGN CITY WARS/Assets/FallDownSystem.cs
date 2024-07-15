using UnityEngine;
using Photon.Pun;
public class FallDownSystem : MonoBehaviour
{
    private Animator animator;
    private MainCharacterController mainCharacterController;
    private CharacterController characterController;
    private PhotonView PV;
    public bool Fell = false;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        mainCharacterController = GetComponent<MainCharacterController>();
        characterController = GetComponent<CharacterController>();
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
    }


    public void GotUp()
    {
   
            animator.SetBool("FellDown", false);
            animator.SetLayerWeight(3, 0);
            characterController.enabled = true;
        GetComponent<PlayerActionsVar>().canfire = true;
        Fell = false;
        if (PV.IsMine)
        {
            mainCharacterController.enabled = true;        
        }




    }

}
