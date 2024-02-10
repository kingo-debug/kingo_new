using UnityEngine;

public class WeaponType : MonoBehaviour
{
    public string WeaponName;
    public Sprite WeaponPFP;
    public int Weapontype;
    private PlayerActionsVar actions;
    private Animator animator;

        private void OnEnable()
    {
        actions = transform.root.GetChild(0).GetComponent<PlayerActionsVar>();
        animator = transform.root.GetChild(0).GetComponent<Animator>();
        WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
        Status.CurrentWeapon = this.gameObject;
        actions.Weapontype = Weapontype;
        animator.SetInteger("WeaponType", Weapontype);
    }
    private void Start()
    {
        WeaponStatus Status = transform.root.GetChild(0).GetComponent<WeaponStatus>();
        Status.CurrentWeapon = this.gameObject;
    }
}
