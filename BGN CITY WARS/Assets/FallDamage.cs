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
              else if (FallingTime > MinSpeedCheck && FallingTime < MidSpeedCheck && other.gameObject.layer!= 4)// min damage
            {
                CallMinDamage();
            }
            


            else if (FallingTime > MidSpeedCheck && FallingTime < MaxSpeedCheck&& other.gameObject.layer != 4) // mid damage
            {
                CallMidDamage();              
            }
        
            else if (FallingTime > MaxSpeedCheck && other.gameObject.layer != 4) // maxDamage
            {
                CallMaxDamage();          
            }
         
            
        }
         
    }


    void CallMinDamage()
    {
        takedamage.Takedamage(MinDamage);
      
    }
    void CallMidDamage()
    {
        takedamage.Takedamage(MidDamage);
    }
    void CallMaxDamage()
    {
        takedamage.Takedamage(MaxDamage);
    }

    private void Update()
    {
        if (!Mainchar.isGrounded &&! jpmanager.Accelerating && ! SwimControl.Swiming)
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
