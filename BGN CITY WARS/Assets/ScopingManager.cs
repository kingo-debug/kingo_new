using UnityEngine;

public class ScopingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerMesh;
    public GameObject WeaponMesh;
    public bool CanScope;

    [SerializeField]
    private GameObject ScopeUI;


    public void ScopeOn()
    {

        if (ScopeUI != null)
        {
            PlayerMesh.SetActive(false);
            WeaponMesh.SetActive(false);
            ScopeUI.SetActive(true);
        }
        else
        {
            //  Debug.LogWarning("ScopeUI is not assigned. Make sure to assign it in the Inspector.");
        }

    }
    public void ScopeOff()
    {
        if (ScopeUI != null)
        {
            PlayerMesh.SetActive(true);
            WeaponMesh.SetActive(true);
            ScopeUI.SetActive(false);
        }

    }


}
