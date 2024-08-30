using UnityEngine;

public class Enableothers : MonoBehaviour
{
   
    public Transform[] Others;
    [SerializeField]
    private bool OnEnabled = false;
    private void OnEnable()
    {
        if (OnEnabled)
        {
            foreach (Transform other in Others)
            {
                other.gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        if (!OnEnabled)
        {
            foreach (Transform other in Others)
            {
                other.gameObject.SetActive(true);
            }
        }
    }

}
