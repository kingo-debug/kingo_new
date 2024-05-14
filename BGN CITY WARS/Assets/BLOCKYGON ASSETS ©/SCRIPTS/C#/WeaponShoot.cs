﻿using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class WeaponShoot : MonoBehaviour
{

    #region Variables
    private WeaponStatus weaponstatus;
    private WeaponType weapontype;
    [SerializeField]
    private PlayerActionsVar Parentvariables;
    [Header("Weapon Specs")]
    [SerializeField]
    private float PullOutTime;
    public float FireRate;
    public float ReloadTime;
    public int BodyDamage;
    public int HeadDamage;
    public int TotalDamageDealt;
    public float WeaponRange;
    private UpdateKillDisplay Killcountupdate;
    private UpdateAmmoUI UpdateAmmoUI;
    private PlayerScores ScoreItem;


    [Space(10)]
    [Header("Ammo Settings")]

    public int currentclip;
    public int MaxClip;
    public int totalammo;
    public int MaxAmmo;
    public int BulletsFired = 0;
    public bool Lowammo;
    public bool noammo;
    private float lastshot = 0f;
    [Space(10)]
    [Header("Firing Info")]
    public bool Canfire = false;
    private bool started;
    public bool Fired;
    public float modifiedFireRate;
    [Space(10)]
    [Header("Reload Info")]
    public bool Reloading;





    [HideInInspector]
    public bool bodyshotHit;
    [HideInInspector]
    public bool headshotHit;
    [Header("Weapon Settings")]
    [SerializeField]
    private LayerMask layermask;
    private Vector3 point;
    private Transform pos;
    private Transform Shootpoint;
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
    // VFX SPAWN
    [Header("WeaponVFX")]
    public ParticleSystem BulletTrailVFX;
    public ParticleSystem BulletDropVFX;
    public GameObject BulletHoleVFX;
    [SerializeField]
    private GameObject FireVFX; 
    private GameObject CameraMain;
    private CameraAddOns CamAddons;
    private GameObject ScopeUI;
    private Animator ScopeAnimator;
    private Animator Parentanimator;
    [Space(10)]
    [Header("Recoil")]
    [SerializeField]
    private Transform DefaultReticle;
    [SerializeField]
    private WeaponRecoil RecoilManager;

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
    private TextMeshProUGUI AmmoMessage;
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
        ScopeUI = DefaultReticle.transform.GetChild(3).gameObject;
        ScopeAnimator = ScopeUI.GetComponent<Animator>();
        #endregion

     
      AS = GetComponent<AudioSource>();

        #region find  and assign kill pop up feeds.
        KillFeed = GameObject.Find("KILL FEEDS").transform.GetChild(0).gameObject;
        HeadShotKill = GameObject.Find("KILL FEEDS").transform.GetChild(1).gameObject;

        #endregion
        Invoke("FindParent", .15f);
        PV = this.GetComponent<PhotonView>();
        if(!PV.IsMine)
        {
            GetComponent<WeaponShoot>().enabled = false;
        }

        SyncFireAnim();
        Parentanimator.SetFloat("ReloadSpeed",ReloadTime*2.5f);
        collided = hit.collider;
   
      
    

        // start reload after weapon pull//
        if (currentclip < 1 && totalammo>0)
        {
            StartCoroutine(Reload());
        }
     
    }
    private void OnDisable()
    {
        Reloading = false;
        bodyshotHit = false;
        headshotHit = false;
        Parentanimator.SetBool("RELOAD", false);
        AmmoMessage.text = ("");
        Canfire = false;



    }
    void FindParent()
    {
        PlayerParent = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
        weaponstatus = PlayerParent.GetComponent<WeaponStatus>();
        Parentvariables = PlayerParent.GetComponent<PlayerActionsVar>();
        Parentvariables.Fired = Fired;
        Parentanimator = PlayerParent.GetComponent<Animator>();
        UpdateAmmoUI.UpdateAmmoUIDisplay(currentclip, totalammo);
        swimcontrol = PlayerParent.GetComponent<SwimPlayerControl>();



        AmmoRefresh();




    }
    private void Start()
    {
        DamageDelay = 0.25f;
        TargetHP = 100;
        currentclip = MaxClip;
        totalammo = MaxAmmo;
        AmmoMessage = GameObject.Find("AMMO MESSAGE").GetComponent<TextMeshProUGUI>();
        Shootpoint = GameObject.FindGameObjectWithTag("ShootPoint").transform;
        CameraMain = Camera.main.gameObject;   pos = CameraMain.transform.GetChild(2);
        CamAddons = CameraMain.GetComponent<CameraAddOns>();
        SyncFireAnim();
        Parentanimator.SetFloat("ReloadSpeed", ReloadTime * 2.5f);

        Killcountupdate = GameObject.Find("KILL COUNT TEXT DISPLAY").GetComponent<UpdateKillDisplay>();
        ScoreItem = PlayerParent.GetComponent<PlayerActionsVar>().ScoreItemUI.gameObject.GetComponent<PlayerScores>();
        UpdateAmmoUI = PlayerParent.transform.Find("PLAYER Canvas").Find("WEAPON UI INFO").Find("AMMO").GetComponent<UpdateAmmoUI>();
        mainCharacterController = PlayerParent.GetComponent<MainCharacterController>();


    }

    void Update()
    {//update S
     //var sync with plaayer

        PlayerParent.GetComponent<WeaponStatus>().CurrentClip = currentclip;
        PlayerParent.GetComponent<WeaponStatus>().TotalAmmo = totalammo;


        modifiedFireRate = 1.0f / FireRate;

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
            FireVFX.SetActive(false);
            Parentvariables.Fired = false;
            Parentanimator.SetBool("shoot", false);

        }


        if (Canfire)
        { //canfire

            if (ControlFreak2.CF2Input.GetMouseButton(0) == true ) // first condition

            { 
               if( PV.IsMine && Time.time > lastshot + modifiedFireRate && currentclip > 0 && !Reloading && Canfire&& !swimcontrol.Swiming && !mainCharacterController.Rolling)
                {
                    AS.PlayOneShot(FireSFX, 1f);

                    StartCoroutine(VFX());

                    float nextActionTime = 0.0f;
                    float period = 5f;
                    if (Time.time > nextActionTime)
                    {
                        nextActionTime += period;
                        Shoot();
                    }
                } // other conditions

            }

            
     
            
        }//canfire


        else
        {
            return;
        }

        ///check reload conditions///

        //auto reload

        if (currentclip == 0 & !Reloading && !noammo && totalammo > 0)
        {
            StartCoroutine(Reload());
        }
        //Manual reload
        if (ControlFreak2.CF2Input.GetKey(KeyCode.R) && !Reloading && !noammo && currentclip < MaxClip && totalammo > 0)
        {
            StartCoroutine(Reload());

        }


    }//update E


    void Shoot()

    {
        #region AtShootActions !!!! ^
        ScopeAnimator.SetTrigger("fired");
        
        started = true;
        Fired = true;
        Parentvariables.Fired = true;
        Parentanimator.SetBool("shoot", true);
        FireVFX.SetActive(true);
        #endregion
        #region Reticle Recoils
        RecoilManager.AddReticleRecoid();
        #endregion
        #region CamShakes
        CamAddons.AddFireRecoil();
        #endregion

        //track shots fired
        BulletsFired += 1;

        //subtract bullets
        currentclip--;

        //Reset FireRate

        lastshot = Time.time;

        //  SparkleVFX.SetActive(false);

        // Fire RayCast//
        Physics.Raycast(Shootpoint.position, pos.forward, out hit, WeaponRange, layermask);



        collided = hit.collider;

        point = (hit.point);
        started = false;

        //UpdateAmmoAfterShoot
        AmmoRefresh();
        UpdateAmmoUI.UpdateAmmoUIDisplay(currentclip,totalammo);



        if (collided == null || collided.gameObject.layer == 0)

        { return; }
        else

        { TPV = collided.transform.root.transform.GetChild(0).GetComponentInParent<PhotonView>();

            //bullet HOLE SPAWN 
            if (BulletHoleVFX != null)

            {
                GameObject.Instantiate(BulletHoleVFX, hit.point, transform.localRotation);
            }


            //Call Methods

            BodyShot();

            HeadShot();
            #region AfterShootActions
            UpdateAmmoUI.UpdateAmmoUIDisplay(currentclip, totalammo);
            #endregion
        }

        //check Bodyshot

        void BodyShot()


        { // SF


            if (collided != null && collided.name == "HIT BOX-BODY")


            {

                if (TPV != null)
                    //self shoot detect
                    if (TPV.IsMine && TPV.gameObject.tag != ("CAR"))
                        return;
                    else // other online player detect
                    {
                        TargetHP = TPV.GetComponent<TakeDamage>().HP;
                        if (TargetHP > 0)
                        {
                            AS.PlayOneShot(BodyshotSFX, 1f);

                            RpcTarget RPCTYPE = new RpcTarget();
                            if (TPV.IsMine && TPV.gameObject.tag == ("CAR"))
                            {
                                RPCTYPE = RpcTarget.All;
                            }
                            else RPCTYPE = RpcTarget.Others;

                            Bodydamage();

                            Debug.Log("Real Player Detected-Body");

                            HitReticleCrosshair.SetActive(true);
                        }
     
                    }



                else if (collided.name == "HIT BOX-BODY" && TPV == null)
                {
                    ///AI detct
                    if (collided.CompareTag("AI"))

                    {
                        TakeDamage takedamage = collided.transform.GetComponentInParent<TakeDamage>();

                        AS.PlayOneShot(BodyshotSFX, 1f);

                        Debug.Log("AI Target Detected-Body");

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);
                        takedamage.Takedamage(BodyDamage);

                    }

                    else
                    {

                        AS.PlayOneShot(BodyshotSFX, 1f);

                        Debug.Log("Iron Target Detected-Body");

                        TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                        if (takedamage != null)
                        { takedamage.Takedamage(BodyDamage); }

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);


                    }

                }

            }

            else return;

        } //EF
          //check headshot

        void HeadShot()


        { // SF


            if (collided != null && collided.name == "HIT BOX-HEAD")


            {

                if (TPV != null)
                    //self shoot detect
                    if (TPV.IsMine && TPV.gameObject.tag != ("CAR"))
                        return;
                    else // other online player detect
                    {
       
          
                            TargetHP = TPV.GetComponent<TakeDamage>().HP;
                        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
                        if (TargetHP > 0)
                        {
                            AS.PlayOneShot(HeadshotSFX, 1f);

                            RpcTarget RPCTYPE = new RpcTarget();
                            if (TPV.IsMine && TPV.gameObject.tag == ("CAR"))
                            {
                                RPCTYPE = RpcTarget.All;
                            }
                            else RPCTYPE = RpcTarget.Others;

                            Headdamage();

                            //  TPV = collided.GetComponent<PhotonView>();

                            Debug.Log("Real Player Detected-HEAD");

                            //Hit Reticle Enable
                            HitReticleCrosshair.SetActive(true);
                        }

                    }



                else if (collided.name == "HIT BOX-HEAD" && TPV == null)
                {
                    ///AI detct
                    if (collided.CompareTag("AI"))

                    {
                        TakeDamage takedamage = collided.transform.GetComponentInParent<TakeDamage>();

                        AS.PlayOneShot(HeadshotSFX, 1f);

                        Debug.Log("AI Target Detected-HEAD");

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);
                        takedamage.Takedamage(HeadDamage);

                    }

                    else
                    {

                        AS.PlayOneShot(HeadshotSFX, 2.5f);

                        Debug.Log("Iron Target Detected-hEAD");

                        TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                        if (takedamage != null)
                        { takedamage.Takedamage(HeadDamage); }

                        //Hit Reticle Enable
                        HitReticleCrosshair.SetActive(true);


                    }

                }

            }

            else return;

        } //EF

    }//EF


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


    public void AmmoRefil()
    {
        totalammo = MaxAmmo;  noammo = false;
        AmmoRefresh();
        UpdateAmmoUI.UpdateAmmoUIDisplay(currentclip, totalammo);
    }

    private void AmmoRefresh()
    {
        #region NO AMMO SET UP
        //NO AMMO SET UP

        if (weaponstatus.MaxedAmmo)
        {
            totalammo = MaxAmmo;
        }
        if (currentclip < 1 && totalammo == 0)
        {
            noammo = true; PlayerParent.GetComponent<WeaponStatus>().NoAmmo = true; AmmoMessage.color = Color.red; AmmoMessage.text = ("Out Of Ammo");
            BulletsFired = MaxClip;
        }

        else
        {
            {
                noammo = false; PlayerParent.GetComponent<WeaponStatus>().NoAmmo = false; AmmoMessage.text = ("");
            }
        }
        #endregion

        #region LOW AMMO SET UP
        if(totalammo ==0 && currentclip <MaxClip&&!noammo )
        {
            Lowammo = true;
            AmmoMessage.color = Color.yellow;  AmmoMessage.text = ("Low Ammo"); 
        }
        #endregion

        #region RESET BULLETS FIRED
        //Reset BulletsFired Custom conditions(calculate the difference manually)
        if (totalammo == 0)
        {
            BulletsFired = MaxClip - currentclip;
        }


        #endregion


        #region AMMO CLAMP
        if (totalammo <= 0)
        {
            totalammo = 0;
        }
        #endregion

        #region CLIP CLAMP
        //Clip Clamp
        if (currentclip <= 0)
        {
            currentclip = 0;
        }

        if (currentclip >= MaxClip)

        { currentclip = MaxClip; }
        #endregion

        #region RELOADING ON SWITCH
        if (currentclip > 0)
        {
            Reloading = false;
            Parentvariables.IsReloading = false;
        }
        else 
        { Reload(); }
     
        #endregion
    }

    void SyncFireAnim()
    {
        Parentanimator.SetFloat("FireRate", FireRate/3.5f);
    }
    #region /////Coroutines/////

    //Ammo & reload
    IEnumerator Reload()
{
    Reloading = true;           PlayerParent.GetComponent<PlayerActionsVar>().IsReloading = true;     Parentanimator.SetBool("RELOAD", true);

     noammo = false;

        AmmoMessage.color = Color.white;    AmmoMessage.text= ("Reloading..");  


        // Calculate reload time based on the inverse of WeaponType.ReloadSpeed
        float reloadTime = 1.0f / ReloadTime;

    yield return new WaitForSeconds(reloadTime);

    if (totalammo < MaxClip)
    {
        currentclip += totalammo;
        totalammo -= BulletsFired;
        BulletsFired = 0;
        Reloading = false;          PlayerParent.GetComponent<PlayerActionsVar>().IsReloading = false;   Parentanimator.SetBool("RELOAD", false);
            AmmoRefresh();
            UpdateAmmoUI.UpdateAmmoUIDisplay(currentclip, totalammo);
        }
    else
    {
        currentclip += BulletsFired;
        totalammo -= BulletsFired;
        BulletsFired = 0;
        Reloading = false;                   PlayerParent.GetComponent<PlayerActionsVar>().IsReloading = false; Parentanimator.SetBool("RELOAD", false);
            AmmoMessage.text = (""); AmmoMessage.color = new Color(255f, 178f, 255f);
        AmmoRefresh();
            UpdateAmmoUI.UpdateAmmoUIDisplay(currentclip, totalammo);
        }


    }//ef
    //VFX Toggles
    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.15f);

        if(BulletTrailVFX!=null)
        {
            BulletTrailVFX.Play();
        }
       
        BulletDropVFX.Play();
    }
    // Hit Reticles Toggle


    IEnumerator UpdateTargetHP()
    {
        yield return new WaitForSeconds(DamageDelay);
        TargetHP = TPV.GetComponent<TakeDamage>().HP;
        TargetShield = TPV.GetComponent<TakeDamage>().Shield;
   
        if (TargetShield <= 0)
        {
            TotalDamageDealt += BodyDamage;
        }

        if( TargetHP<=0)
        {
            TotalDamageDealt = 0;
        }

    }
    IEnumerator ReadyForFire()
    {
        yield return new WaitForSeconds(PullOutTime);
        Canfire = true;

    }
    #endregion 
}//EC
