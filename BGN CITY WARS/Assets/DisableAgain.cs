
using UnityEngine;

public class DisableAgain : MonoBehaviour
{
    public float  Time=3f;

    private void OnEnable()
    {
        if(Time>0)
        {
            Invoke("Deactivate", Time);
        }

    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
