using UnityEngine;

public class DisableOthers : MonoBehaviour
{

    public Transform[] Others;
    [SerializeField]
    private bool OnDisabled = false;
    private void OnEnable()
    {
        if (!OnDisabled)
    { 
            foreach (Transform other in Others)
            {
                other.gameObject.SetActive(false);
            }
    }
    }

    private void OnDisable()
    {
        if (OnDisabled)
        {
            foreach (Transform other in Others)
            {
                other.gameObject.SetActive(false);
            }
        }
    }

}
