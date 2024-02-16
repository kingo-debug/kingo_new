using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshWeaponStates : MonoBehaviour
{
public void RefreshAllWeaponStates()
    {
        foreach(Transform weapon in gameObject.transform)
        {
            weapon.GetComponent<WeaponOwnershipCheck>().CheckOwnerShipAndEquip();
        }
    }
}
