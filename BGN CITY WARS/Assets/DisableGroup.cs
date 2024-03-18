using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGroup : MonoBehaviour
{
    [SerializeField]
    private Transform [] OthersToDisableWithThis;
    private void OnEnable()
    {
        foreach (Transform other in OthersToDisableWithThis)
        {
            other.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (Transform other in OthersToDisableWithThis)
        {
            other.gameObject.SetActive(false);
        }
    }
}
