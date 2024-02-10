using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
public class InventoryManager : MonoBehaviour
{
    public float SwitchTime = 0.5f;
    [SerializeField]
    private bool Switched = false;
    public List<Transform> Inventory;
    [SerializeField]
    private PlayerActionsVar Actions;
    private PhotonView PV;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        if (PV.IsMine)
        {
            Actions.InventoryTrack = 0;
        }
        RefreshInventory();



    }

    public void RefreshInventory()
    {
        int inventoryTrackInt = Actions.InventoryTrack; // Assuming you have a method to get the inventory track int value
        int index = 0; // Counter variable to keep track of the index

        foreach (Transform item in Inventory)
        {
            // Check if the current index matches the inventory track int
            if (!Switched && index == inventoryTrackInt)
            {
                // Activate the item
                item.gameObject.SetActive(true);
                Switched = true;
                animator.SetTrigger("SWITCH");
                animator.SetInteger("INVENTORY", inventoryTrackInt);
                Invoke("ResetSwitched", SwitchTime);


            }
            else
            {
                // Deactivate the item if it doesn't match the inventory track int
                item.gameObject.SetActive(false);
            }

            // Increment the index for the next iteration
            index++;
        }
    }

    private void ResetSwitched()
    {
        Switched = false;
        animator.ResetTrigger("SWITCH");
    }
}
