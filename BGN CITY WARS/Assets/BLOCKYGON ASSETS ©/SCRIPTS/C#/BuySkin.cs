using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySkin : MonoBehaviour
{

    private OwnedShopItems shopitems;
    private GameObject BuySuccessMessage;
    private GameObject BuyFailedMessage;

    private GameObject EquipButton;

    [SerializeField]
    private AudioClip BuySuccessSFX;
    [SerializeField]
    private AudioClip BuyFailedSFX;

    private AudioSource AS;

    public string SkinID;
    private StatusLoad UIStatus;
    [SerializeField]
    private int Price;
    private void OnEnable()
    {
        shopitems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        EquipButton = transform.parent.GetChild(1).gameObject;
        //check if already owned
        if (shopitems.OwnedSkins.Contains(SkinID))
        {
            EquipButton.SetActive(true); gameObject.SetActive(false);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        UIStatus = GameObject.Find("quick info").GetComponent<StatusLoad>();
        AS = GameObject.Find("MENU SFX").GetComponent<AudioSource>();

        BuySuccessMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(0).gameObject;
        BuyFailedMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(1).gameObject;

      

    }

    public void Buy()
    {

        if (ES3.Load<int>("BGNCoins") >= Price && !shopitems.OwnedSkins.Contains(SkinID))
        {
            BuySuccessMessage.SetActive(true);
            int SubtractedPrice = ES3.Load<int>("BGNCoins") - +Price;
            ES3.Save<int>("BGNCoins", SubtractedPrice);
            AS.PlayOneShot(BuySuccessSFX);
            shopitems.OwnedSkins.Add(SkinID);
            EquipButton.SetActive(true); gameObject.SetActive(false);
            shopitems.SaveSkins();
            UIStatus.LoadStatus();


        }
        else
        {
            BuyFailedMessage.SetActive(true);
            AS.PlayOneShot(BuyFailedSFX);
        }
    }

}
