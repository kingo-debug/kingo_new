using UnityEngine;


public class EquipSkin : MonoBehaviour
{
    private BuySkin buy;
    private InitiateData data;
    [SerializeField]
    private RefreshEquippedStates states;
    private GameObject SkinItem;

    private GameObject EquippedButton;

    private void Start()
    {

        if (buy.SkinID == data.EquippedSkin)

        {
            Equip();
        }
    }

    private void OnEnable()
    {
        if (SkinItem == null || buy == null || states == null | EquippedButton == null | data == null)
        {
            SkinItem = transform.parent.parent.gameObject;

            data = GameObject.Find("ApplicationManager").GetComponent<InitiateData>();
            EquippedButton = transform.parent.GetChild(2).gameObject;
            buy = transform.parent.GetChild(0).GetComponent<BuySkin>();
           states = GameObject.Find("skin window").GetComponent<RefreshEquippedStates>();

        }


        
    }
    public void Equip()
    {
    
       Debug.Log(data.EquippedSkin = buy.SkinID);
        data.SaveStats();
        #region Button Toggles
        EquippedButton.SetActive(true);
        states.EquippedItem.transform.Find("STATE").GetChild(2).gameObject.SetActive(false);    //deactivate equipped button
        states.EquippedItem.transform.transform.Find("STATE").GetChild(1).gameObject.SetActive(true);    //activate equip button

        states.EquippedItem = transform.parent.parent.gameObject;    //update equipped item

        states.EquippedItem.transform.Find("STATE").GetChild(2).gameObject.SetActive(true);    //activate equipped button
        states.EquippedItem.transform.transform.Find("STATE").GetChild(1).gameObject.SetActive(false);    //deactivate equip button

        #endregion




        gameObject.SetActive(false);




        
       

    }


}
