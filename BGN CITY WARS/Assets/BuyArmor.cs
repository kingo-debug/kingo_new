using TMPro;
using UnityEngine;

public class BuyArmor : MonoBehaviour
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

    public string ArmorID;
    private StatusLoad UIStatus;

    // Prices for gold and cash
    [SerializeField]
    private int GoldPrice;
    [SerializeField]
    private int CashPrice;

    // Start is called before the first frame update
    void Start()
    {
        AS = GameObject.Find("SFX").GetComponent<AudioSource>();
        shopitems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        EquipButton = transform.parent.GetChild(1).gameObject;
        BuySuccessMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(0).gameObject;
        BuyFailedMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(1).gameObject;
        UIStatus = GameObject.Find("quick info").GetComponent<StatusLoad>();

        // Display the price: prefer GoldPrice if non-zero, otherwise display CashPrice
        TextMeshProUGUI PriceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        PriceText.text = (GoldPrice > 0 ? GoldPrice.ToString() : CashPrice.ToString());
    }

    public void Buy()
    {
        // Decide whether to use GoldPrice or CashPrice using the ?: operator
        int priceToUse = GoldPrice > 0 ? GoldPrice : CashPrice;

        // Use gold if GoldPrice is non-zero, otherwise use cash for the transaction
        if ((GoldPrice > 0 && ES3.Load<int>("BGNCoins") >= GoldPrice) || (CashPrice > 0 && ES3.Load<int>("BGNCash") >= CashPrice))
        {
            // Success purchase
            BuySuccessMessage.SetActive(true);
            AS.PlayOneShot(BuySuccessSFX);

            // Deduct the appropriate currency
            if (GoldPrice > 0)
            {
                int SubtractedGold = ES3.Load<int>("BGNCoins") - GoldPrice;
                ES3.Save<int>("BGNCoins", SubtractedGold);
            }
            else
            {
                int SubtractedCash = ES3.Load<int>("BGNCash") - CashPrice;
                ES3.Save<int>("BGNCash", SubtractedCash);
            }

            // Add the armor to owned items, enable equip button, and save
            shopitems.OwnedArmors.Add(ArmorID);
            EquipButton.SetActive(true);
            gameObject.SetActive(false);
            shopitems.SaveArmors();
            UIStatus.LoadStatus();
        }
        else
        {
            // Failed purchase
            BuyFailedMessage.SetActive(true);
            AS.PlayOneShot(BuyFailedSFX);
        }
    }
}
