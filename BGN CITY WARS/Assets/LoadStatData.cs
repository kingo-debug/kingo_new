using UnityEngine;
using TMPro;
public class LoadStatData : MonoBehaviour
{
    [SerializeField] private string StatType;
    TextMeshProUGUI Text;
    void OnEnable()
    {
        Text=GetComponent<TextMeshProUGUI>();
        Text.text = ES3.Load<int>(StatType).ToString();
    }
}
