using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarDrowner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="CAR")
        {
            other.GetComponent<CarController>().UnderWater = true;
            other.GetComponent<Rigidbody>().drag = 18;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CAR")
        {
            other.GetComponent<CarController>().UnderWater = false;
            other.GetComponent<Rigidbody>().drag = 0.2f;
        }
    }
}
