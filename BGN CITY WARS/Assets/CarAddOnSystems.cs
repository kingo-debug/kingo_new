
using UnityEngine;

public class CarAddOnSystems : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      float  Accel = ControlFreak2.CF2Input.GetAxis("Vertical");
        animator.SetFloat("Accelerate",Accel);
        bool Braking = ControlFreak2.CF2Input.GetButton("Jump");
        animator.SetBool("Brake", Braking);
    }
}
