
using UnityEngine;

public class DisableAgain : MonoBehaviour
{
    public float  Time=3f;
    private void OnEnable()
    {
        Invoke("Deactivate", Time);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
