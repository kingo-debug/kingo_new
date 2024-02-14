using TMPro;
using UnityEngine;

public class DataToTextUI : MonoBehaviour
{
    public int Data;
 public void SetTextData()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = Data.ToString();
    }
}
