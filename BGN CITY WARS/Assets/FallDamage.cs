using UnityEngine;
public class FallDamage : MonoBehaviour
{
    private SpeedCheck Speed;
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


    void Start()
    {
        Speed = GetComponent<SpeedCheck>();
        takedamage = GetComponent<TakeDamage>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer== 0 || other.gameObject.layer== 10 || other.gameObject.layer == 4 )
        {
            if (Speed.speed > MinSpeedRoll && Speed.speed < MinSpeedCheck) // Just Roll
            {
                Roll();
            }
              else if (Speed.speed > MinSpeedCheck && Speed.speed < MidSpeedCheck) // min damage
            {
                CallMinDamage();
               
            }
            else if (Speed.speed > MidSpeedCheck && Speed.speed < MaxSpeedCheck) // mid damage
            {
                CallMidDamage();
               
            }
            else if (Speed.speed > MaxSpeedCheck) // maxDamage
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
    void Roll()
    {
        animator.SetLayerWeight(7, 1);
    }
   public void ResetRoll()
    {
        animator.SetLayerWeight(7, 0);
    }
}
