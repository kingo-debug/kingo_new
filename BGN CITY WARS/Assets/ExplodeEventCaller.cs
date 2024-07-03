using UnityEngine;

public class ExplodeEventCaller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TakeDamage takedamage;
    [SerializeField]
    private float HPtoSmoke = 40;
    [SerializeField]
    private float HPtoFire = 10;
    [SerializeField]
    private Transform SmokeVFX;
    [SerializeField]
    private Transform FireVFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(takedamage.HP<= HPtoSmoke)
        {

        }
        
    }
}
