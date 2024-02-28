using UnityEngine;
using TMPro;

public class UpdateKillDisplay : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateKillCount(0);

    }
    public void UpdateKillCount(int Kills)
    {
        text.text = Kills.ToString();
    }


}
