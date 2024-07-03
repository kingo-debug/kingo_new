using UnityEngine;

public class ExplodeEventCaller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TakeDamage takedamage;
    [SerializeField]
    private float SmokeHP = 40;
    [SerializeField]
    private float FireHP = 10;
    [Header("VFX SYSTEM")]
    [SerializeField]
    private Transform SmokeVFX;
    [SerializeField]
    private Transform FireVFX;
    [SerializeField]
    private Transform ExplodeVFX;


    [Header("BodySystem")]
    [SerializeField]
    private Transform DefaultBody;

    [SerializeField]
    private Transform DestroyedBody;

    [Header("FireSettings")]
    [SerializeField]
    private float DefaultFireDestroyStartTime = 10;
    [SerializeField]
    private float CurrentFireDestroyStartTime = 10;
    [SerializeField]
    private float OnFireDestroySpeed = 10;

    void Start()
    {
        CurrentFireDestroyStartTime = DefaultFireDestroyStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(FireVFX.gameObject.activeSelf)
        {
            float timer = CurrentFireDestroyStartTime - OnFireDestroySpeed * Time.deltaTime;
            if(timer<=0)
            {
                Explode();
            }
        }
    }

    private void OnEnable()
    {
        if(takedamage.HP<= SmokeHP && takedamage.HP > FireHP)
        {
            SmokeVFX.gameObject.SetActive(true);
            FireVFX.gameObject.SetActive(false);
        }
        else if(takedamage.HP > FireHP)
        {
            SmokeVFX.gameObject.SetActive(false);
            FireVFX.gameObject.SetActive(true);
        }
    }

    void Explode()
    {
        //vfx play
        FireVFX.gameObject.SetActive(false);
        ExplodeVFX.gameObject.SetActive(true);
        //time reset
        CurrentFireDestroyStartTime = DefaultFireDestroyStartTime;
        //BodyReplace
        DefaultBody.gameObject.SetActive(false);
        DestroyedBody.gameObject.SetActive(true);
    }
}
