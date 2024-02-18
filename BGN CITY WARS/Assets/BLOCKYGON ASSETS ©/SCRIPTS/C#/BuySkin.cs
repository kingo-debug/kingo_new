using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySkin : MonoBehaviour
{

    private InitiateData Data;
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

    [SerializeField]
    private int Price;

    // Start is called before the first frame update
    void Start()
    {
        Data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
        AS = GameObject.Find("MENU SFX").GetComponent<AudioSource>();
        shopitems = GameObject.Find("SHOP MENU").GetComponent<OwnedShopItems>();
        EquipButton = transform.parent.GetChild(1).gameObject;
        BuySuccessMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(0).gameObject;
        BuyFailedMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(1).gameObject;

        if (shopitems.OwnedSkins.Contains(SkinID))
        {
            EquipButton.SetActive(true); gameObject.SetActive(false);
        }

    }

    public void Buy()
    {

        if (Data.BGNCoins >= Price && !shopitems.OwnedSkins.Contains(SkinID))
        {
            BuySuccessMessage.SetActive(true);
            Data.BGNCoins -= Price;
            AS.PlayOneShot(BuySuccessSFX);
            shopitems.OwnedSkins.Add(SkinID);
            shopitems.SaveSkins();
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
