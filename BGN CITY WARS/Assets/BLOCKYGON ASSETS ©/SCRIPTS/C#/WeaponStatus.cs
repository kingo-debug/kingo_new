using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
 public int CurrentClip;
 public int TotalAmmo;
 public bool MaxedAmmo;
 public bool NoAmmo;
    public GameObject CurrentWeapon;

void Update() 
{
    if(MaxedAmmo)
    {
        Invoke("ResetMxedAmmo",0.15f);
    }
}
void ResetMxedAmmo()
{
 MaxedAmmo =false;
}
    
}
