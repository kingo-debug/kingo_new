using UnityEngine;
using Photon.Pun;
public class FallDownSystem : MonoBehaviour
{
    private Animator animator;
    private MainCharacterController mainCharacterController;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
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
    }


    public void GotUp()
    {
        animator.SetBool("FellDown", false);
        animator.SetLayerWeight(3, 0);
        mainCharacterController.enabled = true;
        characterController.enabled = true;



    }

}
