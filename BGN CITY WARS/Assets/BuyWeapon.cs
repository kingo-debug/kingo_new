using TMPro;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    private OwnedShopItems shopitems;
    private GameObject buySuccessMessage;
    private GameObject buyFailedMessage;
    private GameObject equipButton;

    [SerializeField]
    private AudioClip buySuccessSFX;
    [SerializeField]
    private AudioClip buyFailedSFX;

    private AudioSource audioSource;

    public string WeaponID;
    private StatusLoad uiStatus;

    // Prices for gold and cash
    [SerializeField]
    private int goldPrice;
    [SerializeField]
    private int cashPrice;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
        shopitems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        equipButton = transform.parent.GetChild(1).gameObject;
        buySuccessMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(0).gameObject;
        buyFailedMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(1).gameObject;
        uiStatus = GameObject.Find("quick info").GetComponent<StatusLoad>();

        // Display the price: prefer GoldPrice if non-zero, otherwise display CashPrice
        TextMeshProUGUI priceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        priceText.text = (goldPrice > 0 ? goldPrice.ToString() : cashPrice.ToString());
    }

    public void Buy()
    {
        // Decide whether to use GoldPrice or CashPrice
        int priceToUse = goldPrice > 0 ? goldPrice : cashPrice;
        string currencyKey = goldPrice > 0 ? "BGNCoins" : "BGNCash";

        // Check if the player has enough of the selected currency and doesn't already own the weapon
        if ((goldPrice > 0 && ES3.Load<int>("BGNCoins") >= goldPrice) ||
            (cashPrice > 0 && ES3.Load<int>("BGNCash") >= cashPrice) &&
            !shopitems.OwnedWeapons.Contains(WeaponID))
        {
            // Success purchase
            buySuccessMessage.SetActive(true);
            audioSource.PlayOneShot(buySuccessSFX);

            // Deduct the appropriate currency
            if (goldPrice > 0)
            {
                int newGoldBalance = ES3.Load<int>("BGNCoins") - goldPrice;
                ES3.Save<int>("BGNCoins", newGoldBalance);
            }
            else
            {
                int newCashBalance = ES3.Load<int>("BGNCash") - cashPrice;
                ES3.Save<int>("BGNCash", newCashBalance);
            }

            // Add the weapon to owned items, enable equip button, and save
            shopitems.OwnedWeapons.Add(WeaponID);
            equipButton.SetActive(true);
            gameObject.SetActive(false);
            shopitems.SaveWeapons();
            uiStatus.LoadStatus();
        }
        else
        {
            // Failed purchase
            buyFailedMessage.SetActive(true);
            audioSource.PlayOneShot(buyFailedSFX);
        }
    }
}
