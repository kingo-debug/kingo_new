using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPreviewItem : MonoBehaviour
{
 public void SpawnPreviewObject(GameObject prefab)
    {
        if(transform.childCount>0)
        {
            GameObject.Destroy(transform.GetChild(0).gameObject);
            GameObject.Instantiate(prefab, transform.parent = transform);
        }
       
    }
}
