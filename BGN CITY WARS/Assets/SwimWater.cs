using UnityEngine;

public class SwimWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<SwimPlayerControl>().SwimModeEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<SwimPlayerControl>().SwimmodeExit();
    }
}
