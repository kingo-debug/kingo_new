using UnityEngine;
using UnityEngine.UI;

public class CarSpawnerUIupdate : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField]
    private Image VisualImage;
    [SerializeField]
    private float Value;
    [SerializeField]
    private float ConvertAmount;
    private float Converted;
    private VehicleCoolDown VCD;
    private void Start()
    {
        VCD = GameObject.Find("VehicleCoolDown").GetComponent<VehicleCoolDown>();
          
    }
    void Update()
    {
        Value = VCD.SpawnTimeValue;
        Converted = Value / ConvertAmount;
        VisualImage.fillAmount = Converted;
    }
}
