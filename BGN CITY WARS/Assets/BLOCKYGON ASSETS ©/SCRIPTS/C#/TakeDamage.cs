using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    private GameObject DieUi;

    [Header("Event")]
    public Transform ActivateEvent;


    private void Start()
    {
        int MaxHP = 100;
        if(MaxHPStart)
        {
            HP = MaxHP;
        }
     
        pv = this.GetComponent<PhotonView>();
        if(CanDie)
        {
            animator = GetComponent<Animator>();
        }
    }


    [PunRPC]
    public void Takedamage(int Damage)
    {
        LastDamageTook = Damage;
        hurt = true;

        if (ActivateEvent.transform != null)
        {
            ActivateEvent.gameObject.SetActive(true);
        }
     

        if (HP > 0 && pv != null)
        {
            if (Shield <= 0f & pv.IsMine)
            {
                HP -= Damage;   Refreshbar.UpdateHP(HP);    StartCoroutine("Checklife");
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;           HPcap();  if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };    StartCoroutine("Checklife");
                }
                else
                {
                    Shield -= Damage;
                }
            }
        }
        else
        {
            if (Shield <= 0f)
            {
                HP -= Damage;                            HPcap(); if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };        StartCoroutine("Checklife");
            }
            else
            {
                if (Shield < Damage)
                {
                    int remainingDamage = Damage - Shield;
                    Shield = 0;
                    HP -= remainingDamage;       HPcap(); if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };           StartCoroutine("Checklife");
                }
                else
                {
                    Shield -= Damage; 
                }
            }
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
        HP = 100; if (Refreshbar != null) { Refreshbar.UpdateHP(HP); };
    }

    public IEnumerator Checklife()
    {
        if(pv.IsMine&& HP<1&& CanDie)
        {
            Dead = true;
            animator.SetLayerWeight(3, 1f);
            animator.SetBool("DEAD", true);
            DieUi.SetActive(true);

            MainCharacterController mainCharacterController;
            if (TryGetComponent<MainCharacterController>(out mainCharacterController))
            {
                mainCharacterController.enabled = false;
                mainCharacterController.StopAim();
                mainCharacterController.Combatmode = false;
                animator.SetBool("FIRE INPUT", false);
            }


            #region Respawn
            yield return new WaitForSeconds(RespawnTime);

            Dead = false;
            animator.SetLayerWeight(3, 1f);
            animator.SetBool("DEAD", false);

            if (TryGetComponent<MainCharacterController>(out mainCharacterController))
            {
                Transform spawnpoints = GameObject.FindWithTag("SP").transform;
                transform.position = spawnpoints.GetChild(Random.Range(0, spawnpoints.childCount)).position;
                mainCharacterController.enabled = true;
            }
            HP = 100;
            Refreshbar.UpdateHP(HP);
            DieUi.SetActive(false);

            #endregion

        }
    }
}
