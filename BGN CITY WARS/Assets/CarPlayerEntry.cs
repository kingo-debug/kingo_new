using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerEntry : MonoBehaviour
{
 void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "InRange");
    }
}
