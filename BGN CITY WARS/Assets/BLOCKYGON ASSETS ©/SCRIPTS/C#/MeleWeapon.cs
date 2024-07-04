using System.Collections;
using UnityEngine;
using Photon.Pun;
public class MeleWeapon : MonoBehaviour
{

    #region Variables
    private WeaponStatus weaponstatus;
    private WeaponType weapontype;
    [SerializeField]
    private PlayerActionsVar Parentvariables;
    [Header("Weapon Specs")]
    [SerializeField]
    private float PullOutTime;
    public float SwingSpeed = 0.5f;
    public float SwingSize = .5f;
    public int BodyDamage;
    public int HeadDamage;
    public int VehcileDamage;
    public int TotalDamageDealt;
    public float AttackRange;
    private UpdateKillDisplay Killcountupdate;
    private PlayerScores ScoreItem;


    [Space(10)]


   
    public int BulletsFired = 0;
    private float lastshot = 0f;
    [Space(10)]
    [Header("Firing Info")]
    public bool Canfire = false;
    private bool started;
    public bool Fired;
    public float modifiedFireRate;
    [Space(10)]



    [HideInInspector]
    public bool bodyshotHit;
    [HideInInspector]
    public bool headshotHit;
    [Header("Weapon Settings")]
    [SerializeField]
    private LayerMask layermask;
    private Transform pos;
    private Transform AttackPoint;
    private RaycastHit hit;
    private RaycastHit hit2;

    [Header("Weapon Audio")]
    private AudioSource AS;
    [SerializeField]
    private AudioClip FireSFX;
    [SerializeField]
    private AudioClip BodyshotSFX;
    [SerializeField]
    private AudioClip HeadshotSFX;


    private Transform PlayerParent;

 
    [SerializeField]
    private GameObject CameraMain;
    private Animator Parentanimator;
    [Space(10)]

    [SerializeField]
    private Transform DefaultReticle;


    //pun variables
    [Header("Debugs")]
    private PhotonView PV;
    private Collider collided;
    private GameObject KillFeed;
    private GameObject HeadShotKill;
    private int TargetHP = 100;
    private int TargetShield;
    private PhotonView TPV;
    private bool hasExecutedKill = false;
    public float DamageDelay;
    private int LastDamageType;
    private GameObject HitReticleCrosshair;
    private SwimPlayerControl swimcontrol;
    private MainCharacterController mainCharacterController;

    #endregion Variables

    private void OnEnable()
    {
        StartCoroutine(ReadyForFire());
        #region CrosshairSetUp
        weapontype = GetComponent<WeaponType>();
        DefaultReticle = GameObject.Find("CROSSHAIRS").transform.GetChild(weapontype.ReticleType);
        HitReticleCrosshair = DefaultReticle.transform.GetChild(1).gameObject;
        #endregion


        AS = GetComponent<AudioSource>();

        #region find  and assign kill pop up feeds.
        KillFeed = GameObject.Find("KILL FEEDS").transform.GetChild(0).gameObject;
        HeadShotKill = GameObject.Find("KILL FEEDS").transform.GetChild(1).gameObject;

        #endregion
        Invoke("FindParent", 0.15f);
        PV = this.GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            GetComponent<MeleWeapon>().enabled = false;
        }

        SyncFireAnim();
        collided = hit.collider;




  

    }
    private void OnDisable()
    {
        bodyshotHit = false;
        headshotHit = false;
        Parentanimator.SetBool("RELOAD", false);
        Canfire = false;



    }
    void FindParent()
    {
        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        weaponstatus = PlayerParent.GetComponent<WeaponStatus>();
        Parentvariables = PlayerParent.GetComponent<PlayerActionsVar>();
        Parentvariables.Fired = Fired;
        Parentanimator = PlayerParent.GetComponent<Animator>();
        swimcontrol = PlayerParent.GetComponent<SwimPlayerControl>();

        mainCharacterController = PlayerParent.GetComponent<MainCharacterController>();
        AttackPoint = PlayerParent.transform.Find("Points").transform.Find("Attack Point");
        CameraMain = Camera.main.gameObject;
        ScoreItem = PlayerParent.GetComponent<PlayerActionsVar>().ScoreItemUI.gameObject.GetComponent<PlayerScores>();
        SyncFireAnim();
    }
    private void Start()
    {
        DamageDelay = 0.25f;
        TargetHP = 100;

        Killcountupdate = GameObject.Find("KILL COUNT TEXT DISPLAY").GetComponent<UpdateKillDisplay>();
     

   


    }

    void Update()
    {//update S
     //var sync with plaayer


        modifiedFireRate = 1.0f / SwingSpeed;

        if (TargetShield < 1 && TotalDamageDealt >= 100)//ResetTotalDamage
        {

            TotalDamageDealt = 0;

        }

        if (TargetHP <= 0 && !hasExecutedKill && PV.IsMine)
        {
            if (LastDamageType == 0)
            {
                Kill();
                hasExecutedKill = true; // Set the flag to indicate the code has executed
            }
            else if (LastDamageType == 1)
            {
                HeadKill();
                hasExecutedKill = true; // Set the flag to indicate the code has executed
            }
        }


        if (Time.time > lastshot + 0.2f)
        {
            Fired = false;
            Parentvariables.Fired = false;
            Parentanimator.SetBool("shoot", false);

        }


        if (Canfire)
        { //canfire

            if (ControlFreak2.CF2Input.GetMouseButton(0) == true) // first condition

            {
                if (PV.IsMine && Time.time > lastshot + modifiedFireRate &&  Canfire && !swimcontrol.Swiming && !mainCharacterController.Rolling)
                {
                    AS.PlayOneShot(FireSFX, 1f);

                    float nextActionTime = 0.0f;
                    float period = 5f;
                    if (Time.time > nextActionTime)
                    {
                        nextActionTime += period;
                        Attack();
                    }
                } // other conditions

            }

        }//canfire


        else
        {
            return;
        }


    }//update E


    void Attack()

    {
        #region AtShootActions !!!! ^
        started = true;
        Fired = true;
        Parentvariables.Fired = true;
        Parentanimator.SetBool("shoot", true);
        #endregion






        lastshot = Time.time;



        // Fire RayCast//
        Physics.SphereCast(AttackPoint.position, SwingSize, PlayerParent.forward, out hit, AttackRange, layermask);



        collided = hit.collider;
        started = false;



        if (collided == null || collided.gameObject.layer == 0)

        { return; }
        else

        {
            TPV = collided.transform.root.transform.GetChild(0).GetComponentInParent<PhotonView>();


            //Call Methods

            BodyShot();

            HeadShot();

        }

        //check Bodyshot

        void BodyShot()
        { // SF


            if (collided != null && collided.name == "HIT BOX-BODY") //bodyshot
            {
                if (TPV == null)
                {
                    IronTarget();
                }
                else if (TPV.IsMine && collided.CompareTag("Player"))
                {
                    return; // self shoot Detect
                }

                else if (!TPV.IsMine && collided.CompareTag("Player"))
                {
                    RealPlayer(); // Other Player Detect
                }
                else if (collided.CompareTag("CAR"))
                {
                    CarDetect(); // car detected
                }

            }


            void IronTarget()
            {
                AS.PlayOneShot(BodyshotSFX, 1f);

                Debug.Log("Iron Target Detected-Body");

                TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                if (takedamage != null)
                { takedamage.Takedamage(BodyDamage); }

                //Hit Reticle Enable
                HitReticleCrosshair.SetActive(true);
            }

            void RealPlayer()
            {
                TargetHP = TPV.GetComponent<TakeDamage>().HP;
                if (TargetHP > 0)
                {
                    AS.PlayOneShot(BodyshotSFX, 1f);


                    Bodydamage();

                    Debug.Log("Real Player Detected-Body");

                    HitReticleCrosshair.SetActive(true);
                }
            }
            void CarDetect()
            {
                AS.PlayOneShot(BodyshotSFX, 1f);

                Debug.Log("CarDetected");

                //Hit Reticle Enable
                HitReticleCrosshair.SetActive(true);

                TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                if (takedamage != null)
                { takedamage.Takedamage(VehcileDamage); }
            }

        } //EF
          //check headshot

        void HeadShot()
        { // SF


            if (collided != null && collided.name == "HIT BOX-HEAD") //bodyshot
            {
                if (TPV == null)
                {
                    IronTarget();
                }
                else if (TPV.IsMine && collided.CompareTag("Player"))
                {
                    return; // self shoot Detect
                }

                else if (!TPV.IsMine && collided.CompareTag("Player"))
                {
                    RealPlayer(); // Other Player Detect
                }
            }




            void IronTarget()
            {
                AS.PlayOneShot(HeadshotSFX, 1f);

                Debug.Log("Iron Target Detected-Head");

                TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                if (takedamage != null)
                { takedamage.Takedamage(HeadDamage); }

                //Hit Reticle Enable
                HitReticleCrosshair.SetActive(true);
            }

            void RealPlayer()
            {
                TargetHP = TPV.GetComponent<TakeDamage>().HP;
                if (TargetHP > 0)
                {
                    AS.PlayOneShot(HeadshotSFX, 1f);


                    Headdamage();

                    Debug.Log("Real Player Detected-Head");

                    HitReticleCrosshair.SetActive(true);
                }
            }

        }//EF
    }

    void Bodydamage()
    {
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
        LastDamageType = 0;

        if (TargetHP > 0)
        {
            hasExecutedKill = false;
            TPV.RPC("Takedamage", RpcTarget.All, BodyDamage);
            StartCoroutine(UpdateTargetHP());

        }

        Debug.Log("body reached");
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
    }

    void Kill()
    {
        KillFeed.gameObject.SetActive(true);
        Parentvariables.TotalRoomkillsTrack++;
        Killcountupdate.UpdateKillCount(Parentvariables.TotalRoomkillsTrack);
        ScoreItem.UpdateScoreData();
        TotalDamageDealt = 0;


        TargetHP = 100;

        GameObject Killpopupitem = PhotonNetwork.Instantiate("KILLS POPUP ITEM", transform.position, Quaternion.identity); // spawn kill UI notification
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKilled = TPV.GetComponent<PhotonSerializerBGN>().PlayerNickName;
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKiller = PhotonNetwork.NickName;
        hasExecutedKill = false;
    }

    void HeadKill()
    {
        HeadShotKill.gameObject.SetActive(true);
        Parentvariables.TotalRoomkillsTrack++;
        Killcountupdate.UpdateKillCount(Parentvariables.TotalRoomkillsTrack);
        ScoreItem.UpdateScoreData();
        TotalDamageDealt = 0;


        TargetHP = 100;

        GameObject Killpopupitem = PhotonNetwork.Instantiate("KILLS POPUP ITEM", transform.position, Quaternion.identity); // spawn kill UI notification
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKilled = TPV.GetComponent<PhotonSerializerBGN>().PlayerNickName;
        Killpopupitem.GetComponent<KillPopupManager>().PlayerKiller = PhotonNetwork.NickName;
        hasExecutedKill = false;
    }

    void Headdamage()
    {
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
        LastDamageType = 1;
        if (TargetHP > 0)
        {
            hasExecutedKill = false;
            TPV.RPC("Takedamage", RpcTarget.All, HeadDamage);
            StartCoroutine(UpdateTargetHP());

        }

        Debug.Log("Head reached");
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
    }




    void SyncFireAnim()
    {
        Parentanimator.SetFloat("FireRate", SwingSpeed / 3.5f);
    }
    #region /////Coroutines/////








    IEnumerator UpdateTargetHP()
    {
        yield return new WaitForSeconds(DamageDelay);
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;

        if (TargetShield <= 0)
        {
            TotalDamageDealt += BodyDamage;
        }

        if (TargetHP <= 0)
        {
            TotalDamageDealt = 0;
        }

    }
    IEnumerator ReadyForFire()
    {
        yield return new WaitForSeconds(PullOutTime);
        Canfire = true;
    #endregion
}




}//ef


