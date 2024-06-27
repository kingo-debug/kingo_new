using TMPro;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
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

    public string WeaponID;
    private StatusLoad UIStatus;

    [SerializeField]
    private int Price;

    // Start is called before the first frame update
    void Start()
    {
       
        AS = GameObject.Find("SFX").GetComponent<AudioSource>();
        shopitems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        EquipButton = transform.parent.GetChild(1).gameObject;
        BuySuccessMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(0).gameObject;
        BuyFailedMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(1).gameObject;
        UIStatus = GameObject.Find("quick info").GetComponent<StatusLoad>();
        TextMeshProUGUI PriceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        PriceText.text = Price.ToString();

    }

   public  void Buy()
    {

        if (ES3.Load<int>("BGNCoins") >= Price && !shopitems.OwnedWeapons.Contains(WeaponID))
        {
            BuySuccessMessage.SetActive(true);
            int SubtractedPrice = ES3.Load<int>("BGNCoins") - +Price;
            ES3.Save<int>("BGNCoins", SubtractedPrice);
            AS.PlayOneShot(BuySuccessSFX);
            shopitems.OwnedWeapons.Add(WeaponID);
            EquipButton.SetActive(true); gameObject.SetActive(false);
            shopitems.SaveWeapons();
            UIStatus.LoadStatus();
        }
        else
        {
            BuyFailedMessage.SetActive(true);
            AS.PlayOneShot(BuyFailedSFX);
        }
    }
  
}
