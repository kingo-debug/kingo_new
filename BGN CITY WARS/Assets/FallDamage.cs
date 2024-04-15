using UnityEngine;
public class FallDamage : MonoBehaviour
{
    private SpeedCheck Speed;
    private TakeDamage takedamage;
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


    void Start()
    {
        Speed = GetComponent<SpeedCheck>();
        takedamage = GetComponent<TakeDamage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer== 0 || other.gameObject.layer== 10 || other.gameObject.layer == 4 )
        {

            if (Speed.speed > MinSpeedCheck && Speed.speed < MidSpeedCheck)
            {

                CallMinDamage();
            }
            else if (Speed.speed > MidSpeedCheck && Speed.speed < MaxSpeedCheck)
            {
                CallMidDamage();
            }
            else if (Speed.speed > MaxSpeedCheck)
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
}
