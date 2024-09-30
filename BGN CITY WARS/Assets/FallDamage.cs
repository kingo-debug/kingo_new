using UnityEngine;
public class FallDamage : MonoBehaviour
{


    private TakeDamage takedamage;
    [SerializeField]
    private float MinSpeedRoll;
    [SerializeField]
    private float MinSpeedCheck;
    [SerializeField]
    private int MinDamage;
    [SerializeField]
    private float MidSpeedCheck;
    [SerializeField]
    private int MidDamage;
    [SerializeField]
    private float MaxSpeedCheck;
    [SerializeField]
    private int MaxDamage;
    private Animator animator;
    private MainCharacterController Mainchar;
    public float FallingTime;
    [SerializeField]
    private float FallingSpeed = 1f;
    private JetPackManager jpmanager;
    private SwimPlayerControl SwimControl;

    void Start()
    {
        takedamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
        Mainchar = GetComponent<MainCharacterController>();
        jpmanager = GetComponent<JetPackManager>();
        SwimControl = GetComponent<SwimPlayerControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer== 0 || other.gameObject.layer== 10 || other.gameObject.layer == 4 )
        {
            if (FallingTime > MinSpeedRoll && FallingTime < MinSpeedCheck) // Just Roll
            {
                Mainchar.Rolling = true;  
            }
              else if (FallingTime > MinSpeedCheck && FallingTime < MidSpeedCheck )// min damage
            {
                if(other.gameObject.layer != 4) // check if layer is water
                {
                    CallMinDamage(); //if not then damage
                }
                else Mainchar.Rolling = true; // if water then dive using roll method

            }
            


            else if (FallingTime > MidSpeedCheck && FallingTime < MaxSpeedCheck) // mid damage
            {
                if (other.gameObject.layer != 4) // check if layer is water
                {
                    CallMidDamage(); //if not then damage
                }
                else Mainchar.Rolling = true; //  if water then dive using roll method
            }
        
            else if (FallingTime > MaxSpeedCheck) // maxDamage
            {
                if (other.gameObject.layer != 4) // check if layer is water
                {
                    CallMaxDamage(); //if not then damage
                }
                else Mainchar.Rolling = true; //  if water then dive using roll method
            }
         
            
        }
         
    }


    void CallMinDamage()
    {
        takedamage.TakeFallDamage(MinDamage);
      
    }
    void CallMidDamage()
    {
        takedamage.TakeFallDamage(MidDamage);
    }
    void CallMaxDamage()
    {
        takedamage.TakeFallDamage(MaxDamage);
    }

    private void Update()
    {
        if (!Mainchar.isGrounded &&! jpmanager.Accelerating && ! SwimControl.Swiming&& Mainchar.CanMove)
        {
            FallingTime += FallingSpeed * Time.deltaTime;
        }
        else ResetFalling();     
    }


    public void ResetFalling()
    {
        FallingTime = 0f;
    }
}
