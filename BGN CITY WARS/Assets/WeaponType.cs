using UnityEngine;
using System.Collections.Generic;

public class WeaponType : MonoBehaviour
{
    public string WeaponName;
    public Sprite WeaponPFP;
    public int Weapontype;
    public int ReticleType;
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
       foreach (RectTransform child in Crosshiars)
        {
            Reticles.Add(child);
        }
        UpdateReticle();

    }
    private void Start()
    {
        WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
        Status.CurrentWeapon = this.gameObject;

    }

    public void UpdateReticle()
    {
        foreach (RectTransform reticle in Reticles)
        {
            if(reticle.name != ReticleType.ToString()  )
            {
                reticle.gameObject.SetActive(false);
            }
            else
            {
                reticle.gameObject.SetActive(true);
            }
        }
    }
}
