using UnityEngine;

public class SwimWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
