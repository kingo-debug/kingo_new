using UnityEngine;

public class SwimPlayerControl : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public bool Swiming;
    public float SwimEnterSpeed = 2f;
    private void Start()
    {
        SwimmodeExit();
    }
    public void SwimModeEnter()
    { 
        Swiming = true;
        animator.SetBool("SWIMMING", true);
    }

    public void SwimmodeExit()
    {
        Swiming = false;
        animator.SetBool("SWIMMING", false);
    }

    private void Update()
    {
        if(Swiming)
        {
            if (animator.GetLayerWeight(2) < 1)
            {
        
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1.2f, Time.deltaTime * SwimEnterSpeed));
            }
        }
        else
        {
            if(animator.GetLayerWeight(2)>0)
            {
             
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), -0.2f, Time.deltaTime * SwimEnterSpeed));
            }
        }

    }



}
