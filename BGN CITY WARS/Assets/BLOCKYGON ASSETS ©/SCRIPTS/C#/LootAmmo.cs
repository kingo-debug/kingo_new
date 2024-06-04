
using UnityEngine;

public class LootAmmo : MonoBehaviour
{
    public AudioClip PickupSFX;
    [SerializeField]
    private float RespawnTime;
     void OnTriggerEnter(Collider other)
      
    {
       
        
            Debug.Log(other.name + "HAS PICKED UP LOOT");
            other.transform.GetComponent<WeaponStatus>().CurrentWeapon.GetComponent<WeaponShoot>().AmmoRefil();
            other.GetComponent<AudioSource>().PlayOneShot(PickupSFX);
            PickedUp();
        
    }
    private void PickedUp()
    {
        transform.parent.parent.GetComponent<ReEnable>().ReEnableItem(RespawnTime, this.gameObject.transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }

}
