using System.Collections;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class TakeDamage : MonoBehaviour
{
    public int HP = 100;
    public int Shield = 100;
    private PhotonView pv;
    public int LastDamageTook;
    [SerializeField]
    private bool MaxHPStart;
    public bool hurt;
    [SerializeField]
    private bool CanDie = false;
    [SerializeField]
    private bool Dead = false;
    [SerializeField]
    private float RespawnTime = 5f;

    private Animator animator;
    [SerializeField]
    private UIBarRefresh Refreshbar;
    [SerializeField]
    private UIBarRefresh Refreshbar2;

    [SerializeField]
    private GameObject DieUi;
    [SerializeField]
    SpawnPointManager SPM;
    [Header("Event")]
    public Transform ActivateEvent;


    private void Start()
    {
        int MaxHP = 100;
        if (MaxHPStart)
        {
            HP = MaxHP;
            if (Refreshbar != null)
            {
                Refreshbar.UpdateHP(HP);
            }

            if (Refreshbar2 != null)
            {           
                Invoke("DelayBar2Refresh", 0.1f);           
            }
     
        }
        pv = this.GetComponent<PhotonView>();
        if(CanDie)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void DelayBar2Refresh()
    {
        Refreshbar2.UpdateHP(Shield);
    }
    private void OnEnable()
    {
        StartCoroutine(Checklife());
    }

    [PunRPC]
    public void Takedamage(int Damage)
    {
        LastDamageTook = Damage;
        hurt = true;

        if (ActivateEvent != null)
        {
            ActivateEvent.gameObject.SetActive(true);
        }
     

        if (HP > 0 && pv != null)
        {
            if (Shield <= 0f & pv.IsMine)
            {
                HP -= Damage;                         if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };               StartCoroutine("Checklife"); if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;           HPcap();  if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };    StartCoroutine("Checklife"); if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };
                }
                else
                {
                    Shield -= Damage; SHIELDcap(); if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };  
                }
            }
        }
        else
        {
            if (Shield <= 0f)
            {
                HP -= Damage;                            HPcap(); if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };        StartCoroutine(Checklife()); if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;       HPcap(); if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };          StartCoroutine(Checklife()); if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };
                }
                else
                {
                    Shield -= Damage; 
                }
            }
        }
    }


    public void TakeFallDamage(int Damage)
    {
        LastDamageTook = Damage;
        hurt = true;
        if (ActivateEvent != null)
        {
            ActivateEvent.gameObject.SetActive(true);
        }


        if (HP > 0 && pv != null)
        {        
                HP -= Damage; if (Refreshbar != null) { Refreshbar.UpdateHP(HP); }; StartCoroutine("Checklife"); if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };       
        }
    }

    //when hp goes below 0 it sets back to 0
    private void HPcap()
    {
        if (HP <= 0)
            HP = 0;
    }

    //same but for shield
    private void SHIELDcap()
    {
        if (Shield <= 0)
            Shield = 0;
    }
    [PunRPC]
    public void RestoreHP()
    {
        HP = 100; if (Refreshbar != null) { Refreshbar.UpdateHP(HP); }; if (Refreshbar2 != null) { Refreshbar2.UpdateHP(Shield); };
    }

    public IEnumerator Checklife()
    {
        if (pv != null)
        {
            if (pv.IsMine && HP < 1 && CanDie)
            {
                Dead = true;

                animator.SetLayerWeight(3, 1f);
                animator.SetBool("DEAD", true);
                ES3.Save<int>("TotalDeaths", ES3.Load<int>("TotalDeaths")+1);
                if (DieUi != null)
                {
                    DieUi.SetActive(true);
                }


                MainCharacterController mainCharacterController;
                if (TryGetComponent<MainCharacterController>(out mainCharacterController))
                {
                    transform.Find("PLAYER Canvas").transform.Find("SCREEN BLOCKERS").GetChild(0).gameObject.SetActive(true);
                    mainCharacterController.StopAim();
                    mainCharacterController.Combatmode = false;
                    animator.SetBool("FIRE INPUT", false);

                    #region Character Controller ReSize
                    CharacterController characterController = GetComponent<CharacterController>();
                    characterController.radius = 0;
                    characterController.height = 0;
                    characterController.center = new Vector3(0, 0.5f, 0);
                    #endregion

                }


                #region Respawn
                yield return new WaitForSeconds(RespawnTime);

                Dead = false;
                animator.SetLayerWeight(3, 1f);
                animator.SetBool("DEAD", false);

                if (TryGetComponent<MainCharacterController>(out mainCharacterController))
                {            
                    List<Transform> spawnpoints = SPM.spawnPoints;

                    // Set the position to a random Transform from the list
                    transform.position = spawnpoints[Random.Range(0, spawnpoints.Count)].position;

                    // Disable the first child of "SCREEN BLOCKERS" within the "PLAYER Canvas"
                    transform.Find("PLAYER Canvas").Find("SCREEN BLOCKERS").GetChild(0).gameObject.SetActive(false);

                    #region Character Controller ReSize
                    CharacterController characterController = GetComponent<CharacterController>();
                    characterController.radius = 1;
                    characterController.height = 4;
                    characterController.center = new Vector3(0, 1.5f, 0);
                    #endregion
                }
                HP = 100;
                if (GetComponent<ArmorManager>().CurrentBodyArmor!= null)
                {
                    Shield = GetComponent<ArmorManager>().TargetMainBody.transform.GetChild(0).GetComponent<PlayerArmor>().ArmorAmount; // restore Shield to armor Equipped
                }

                Refreshbar.UpdateHP(HP);
                Refreshbar2.UpdateHP(Shield);
                if (DieUi != null)
                {
                    DieUi.SetActive(false);
                }


                #endregion

            }
        }
    }

}
