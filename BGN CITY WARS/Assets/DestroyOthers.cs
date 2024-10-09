using UnityEngine;

public class DestroyOthers : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] Others;
    private void OnDestroy()
    {
        foreach (GameObject item in Others)
        {
            Destroy(item);
        }
    }

}
