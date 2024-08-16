using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class WeaponType : MonoBehaviour
{
    public string WeaponName;
    public Sprite WeaponPFP;
    public Sprite WeaponFireUi;
    public int Weapontype;
    public int ReticleType;
    public bool Scope;
    public bool Melee;
    private PlayerActionsVar actions;
    private Animator animator;
    private List<RectTransform> Reticles;
    private Transform Crosshiars;
    [SerializeField]
    private AudioClip[] ReloadPartsSFX;
    private SFXmanager sfxmanager;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void OnEnable()
    {
        if(PV.IsMine)
        {
            actions = transform.root.GetChild(0).GetComponent<PlayerActionsVar>();
            animator = transform.root.GetChild(0).GetComponent<Animator>();
            WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
            Status.CurrentWeapon = this.gameObject;
            actions.Weapontype = Weapontype;
            animator.SetInteger("WeaponType", Weapontype);
            Crosshiars = GameObject.Find("CROSSHAIRS").transform;
            Invoke("UpdateReticle", 0.1f);
            transform.root.transform.GetChild(0).GetComponent<ScopingManager>().CanScope = Scope;
            if (Scope)
            {
                transform.root.GetChild(0).GetComponent<ScopingManager>().WeaponMesh = transform.GetChild(0).transform.Find("BODY").gameObject;
            }
        }
        Invoke ("UpdateReloadSFX", 0.1f);


    }
    private void Start()
    {
        
        if (PV.IsMine)
        {
            WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
            Status.CurrentWeapon = this.gameObject;
            Crosshiars = GameObject.Find("CROSSHAIRS").transform;
            Invoke("UpdateReticle", 0.1f);
        }      
      
        GetComponent<AudioSource>().volume =ES3.Load<float>("SFX"); ;

    }
    public void UpdateReticle() 
    {

        if (ReticleType == 0)
        {
            Crosshiars.GetChild(0).gameObject.SetActive(true);
        }
        else if (ReticleType == 1)
        {
            Crosshiars.GetChild(1).gameObject.SetActive(true);
        }
        else if (ReticleType == 2)
        {
            Crosshiars.GetChild(2).gameObject.SetActive(true);
        }
       
    }
    public void UpdateReloadSFX()  //  assign reload parts audio  to Player IS IN THIS FUNCTION
    {
        sfxmanager = transform.root.transform.GetChild(0).GetComponent<SFXmanager>();
        sfxmanager.ReloadPart = ReloadPartsSFX; // assign reload parts to Player
    }


}
