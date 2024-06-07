using UnityEngine;


public class EquipSkin : MonoBehaviour
{
    private BuySkin buy;
    private OwnedShopItems data;
    private EquippedState states;




    private void Start()
    {
        buy = transform.parent.transform.GetChild(0).gameObject.GetComponent<BuySkin>();
        data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        states = GameObject.Find("skin window").GetComponent<EquippedState>();

        if (buy.SkinID == data.EquippedSkin)

        {
            Equip();
        }
    }


        
    
    public void Equip()
    {
    
       Debug.Log(data.EquippedSkin = buy.SkinID);
        ES3.Save<string>("CurrentSkin", buy.SkinID);

        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // set new equip button
        #endregion

    }


}
