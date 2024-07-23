using UnityEngine;

public class ClipRester : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform Clip;
    [SerializeField]
    private Transform ClipPlace;
    private void OnDisable()
    {
        Clip.SetParent(ClipPlace);
        Clip.transform.localPosition = new Vector3(0, 0, 0);
        Clip.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Clip.transform.localScale = new Vector3(1, 1, 1);
    }


}
