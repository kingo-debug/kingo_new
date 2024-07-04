using UnityEngine;

public class CarExplodedEvent : MonoBehaviour
{
    [SerializeField]
    private CarPlayerEntry carenter;
    [SerializeField]
    private Transform HitBox;
    private void OnEnable()
    {
        if (carenter.PlayerInCar)
        {
            carenter.Player.GetComponent<TakeDamage>().HP = 0;
            carenter.ExitCar();
            carenter.enabled = false;
            HitBox.gameObject.SetActive(false);
            carenter.ForceOutofRange();

        }
        else
        {
            carenter.enabled = false;
            HitBox.gameObject.SetActive(false);
            carenter.ForceOutofRange();

        }


    }
}
