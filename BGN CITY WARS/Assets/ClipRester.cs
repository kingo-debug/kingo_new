using UnityEngine;
using System.Collections;

public class ClipRester : MonoBehaviour
{
   Transform  Clip ;
    private void Start()
    {
        Clip = transform.GetChild(0);
    }

    private void OnDisable()
    {
        StartCoroutine(SetParentNextFrame());
    }

    private IEnumerator SetParentNextFrame()
    {
        yield return new WaitForEndOfFrame(); // Wait until the end of the frame
        if (Clip != null)
        {
            Clip.SetParent(transform);
        }
    }
}
