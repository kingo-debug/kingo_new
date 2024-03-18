using UnityEngine;
using System.Collections.Generic;

public class WeaponType : MonoBehaviour
{
    public string WeaponName;
    public Sprite WeaponPFP;
    public int Weapontype;
    public int ReticleType;
    public bool Scope;
    private PlayerActionsVar actions;
    private Animator animator;
    private List<RectTransform> Reticles;
    private Transform Crosshiars;




    private void OnEnable()
    {

        actions = transform.root.GetChild(0).GetComponent<PlayerActionsVar>();
        animator = transform.root.GetChild(0).GetComponent<Animator>();
        WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
        Status.CurrentWeapon = this.gameObject;
        actions.Weapontype = Weapontype;
        animator.SetInteger("WeaponType", Weapontype);
        Crosshiars = GameObject.Find("CROSSHAIRS").transform;
        Invoke("UpdateReticle", 0.1f);
        if(Scope)
        {
        transform.root.GetChild(0).GetComponent<ScopingManager>().WeaponMesh = transform.GetChild(0).transform.Find("BODY").gameObject;
        }



    }
    private void Start()
    {
        WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
        Status.CurrentWeapon = this.gameObject;
        Crosshiars = GameObject.Find("CROSSHAIRS").transform;
        Invoke("UpdateReticle", 0.1f);


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

}
