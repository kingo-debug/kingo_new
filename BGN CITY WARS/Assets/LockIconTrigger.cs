using UnityEngine;

public class LockIconTrigger : MonoBehaviour
{
    public GameObject lockIcon;  // The lock icon GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lockIcon.SetActive(true); // Show the lock icon
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lockIcon.SetActive(false); // Hide the lock icon
        }
    }
}
