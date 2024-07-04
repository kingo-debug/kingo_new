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
    [SerializeField]
    private Transform OtherVFX;

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
    private float OnFireDestroySpeed = 1.5f;

    public bool Exploded = false;
    [SerializeField]
    private GameObject ObjectEvent;

    void Start()
    {
        CurrentFireDestroyStartTime = DefaultFireDestroyStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (takedamage.HP <= SmokeHP && takedamage.HP > FireHP && !SmokeVFX.gameObject.activeSelf)
        {
            SmokeVFX.gameObject.SetActive(true);
            FireVFX.gameObject.SetActive(false);
        }
        else if (takedamage.HP <= FireHP)
        {
            SmokeVFX.gameObject.SetActive(false);
            FireVFX.gameObject.SetActive(true);
        }


        if (FireVFX.gameObject.activeSelf && !Exploded)
        {
          CurrentFireDestroyStartTime -= OnFireDestroySpeed * Time.deltaTime; // sub time
            if(CurrentFireDestroyStartTime <= 0)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Exploded = true;
        Debug.Log("Boomed Exploded");
        //vfx play
        FireVFX.gameObject.SetActive(false);
        ExplodeVFX.gameObject.SetActive(true);
        OtherVFX.gameObject.SetActive(true);
        //time reset
        CurrentFireDestroyStartTime = DefaultFireDestroyStartTime;
        //BodyReplace
        DefaultBody.gameObject.SetActive(false);
        DestroyedBody.gameObject.SetActive(true);
        if(ObjectEvent!=null)
        {
            ObjectEvent.SetActive(true);
        }

    }
}
