using UnityEngine;
using TMPro;

public class BuyVehicle : MonoBehaviour
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
    public string VehicleID;
    private StatusLoad uiStatus;

    // Prices for gold and cash
    [SerializeField]
    private int goldPrice;
    [SerializeField]
    private int cashPrice;

    private void OnEnable()
    {
        shopitems = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        equipButton = transform.parent.GetChild(1).gameObject;

        // Check if already owned
        if (shopitems.OwnedVehicles.Contains(VehicleID))
        {
            equipButton.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiStatus = GameObject.Find("quick info").GetComponent<StatusLoad>();
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();

        buySuccessMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(0).gameObject;
        buyFailedMessage = GameObject.Find("SHOP NOTIFICATION").transform.GetChild(1).gameObject;

        // Display the price: prefer GoldPrice if non-zero, otherwise display CashPrice
        TextMeshProUGUI priceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        priceText.text = (goldPrice > 0 ? goldPrice.ToString() : cashPrice.ToString());
    }

    public void Buy()
    {
        // Determine which price and currency to use
        int priceToUse = goldPrice > 0 ? goldPrice : cashPrice;
        string currencyKey = goldPrice > 0 ? "BGNCoins" : "BGNCash";

        // Check if the player has enough of the selected currency and doesn't already own the vehicle
        if ((goldPrice > 0 && ES3.Load<int>("BGNCoins") >= goldPrice) ||
            (cashPrice > 0 && ES3.Load<int>("BGNCash") >= cashPrice) &&
            !shopitems.OwnedVehicles.Contains(VehicleID))
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

            // Add the vehicle to owned items, enable equip button, and save
            shopitems.OwnedVehicles.Add(VehicleID);
            equipButton.SetActive(true);
            gameObject.SetActive(false);
            shopitems.SaveVehicles();
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
