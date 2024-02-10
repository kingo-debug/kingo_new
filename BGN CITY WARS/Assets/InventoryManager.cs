using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
public class InventoryManager : MonoBehaviour
{
    public float SwitchTime = 0.5f;
    [SerializeField]
    private bool Switched = false;
    public List<Transform> Weapons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
