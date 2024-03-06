using UnityEngine;

public class DisableOthers : MonoBehaviour
{
    [SerializeField]
    private Transform[] Others;
    private void OnEnable()
    {
        foreach (Transform other in Others)
        {
            other.gameObject.SetActive(false);
        }
    }
}
