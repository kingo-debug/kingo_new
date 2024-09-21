using UnityEngine;

public class PlayerArmor : MonoBehaviour
{

    public int ArmorAmount;
    public string ArmorName;
    private TakeDamage takedamage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetArmor", 0.05f);
    }

    public void SetArmor()
    {
        takedamage = transform.root.GetChild(0).GetComponent<TakeDamage>();
        takedamage.Shield = ArmorAmount;
        takedamage.DelayBar2Refresh();
    }
}
