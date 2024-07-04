using UnityEngine;

public class CarExplodedEvent : MonoBehaviour
{
    [SerializeField]
    private CarPlayerEntry carenter;
    [SerializeField]
    private Transform HitBox;
    private void OnEnable()
    {
        carenter.enabled = false;
        HitBox.gameObject.SetActive(false);
    }
}
