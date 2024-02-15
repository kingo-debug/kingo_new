using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{

    [SerializeField]
    private InitiateData Data;
    private OwnedShopItems shopitems;
    [SerializeField]
    private GameObject BuySuccessMessage;

    [SerializeField]
    private GameObject BuyFailedMessage;
    [SerializeField]
    private GameObject EquipButton;

    [SerializeField]
    private AudioClip BuySuccessSFX;
    [SerializeField]
    private AudioClip BuyFailedSFX;

    private AudioSource AS;

    public string WeaponID;

    [SerializeField]
    private int Price;

    // Start is called before the first frame update
    void Start()
    {
        AS = GameObject.Find("MENU SFX").GetComponent<AudioSource>();
        shopitems = GameObject.Find("SHOP MENU").GetComponent<OwnedShopItems>();
    }

   public  void Buy()
    {

        if (Data.BGNCoins >= Price && !shopitems.OwnedWeapons.Contains(WeaponID))
        {
            BuySuccessMessage.SetActive(true);
            Data.BGNCoins -= Price;
            AS.PlayOneShot(BuySuccessSFX);
            shopitems.OwnedWeapons.Add(WeaponID);
            shopitems.SaveWeapons();
            Data.SaveStats();
            EquipButton.SetActive(true); gameObject.SetActive(false);
           
        }
        else
        {
            BuyFailedMessage.SetActive(true);
            AS.PlayOneShot(BuyFailedSFX);
        }
    }
  
}
