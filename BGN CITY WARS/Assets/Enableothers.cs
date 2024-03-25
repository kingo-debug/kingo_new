using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enableothers : MonoBehaviour
{
    [SerializeField]
    private Transform[] Others;
    private void OnDisable()
    {
        foreach (Transform other in Others)
        {
            other.gameObject.SetActive(true);
        }
    }


}
