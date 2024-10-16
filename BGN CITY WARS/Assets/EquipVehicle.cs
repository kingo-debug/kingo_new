using UnityEngine;

public class EquipVehicle : MonoBehaviour
{
    private BuyVehicle buy;
    private OwnedShopItems data;
    private EquippedState states;

    private void Start()
    {
        buy = transform.parent.transform.GetChild(0).gameObject.GetComponent<BuyVehicle>();
        data = GameObject.Find("OWNED SHOP ITEMS").GetComponent<OwnedShopItems>();
        states = GameObject.Find("vehicle window").GetComponent<EquippedState>();

        if (buy.VehicleID == data.EquippedVehicle)
        {
            Equip();
        }
    }

    public void Equip()
    {
        Debug.Log(data.EquippedVehicle = buy.VehicleID);
        ES3.Save<string>("CurrentVehicle", buy.VehicleID);

        #region refresh states last equipped
        states.CurrentlyEquipped.gameObject.SetActive(false); // Disable last equipped button
        states.CurrentlyEquipped.transform.parent.GetChild(1).gameObject.SetActive(true); // Enable its equip
        states.CurrentlyEquipped = transform.parent.GetChild(2).gameObject; // Set new equip button
        #endregion
    }
}
