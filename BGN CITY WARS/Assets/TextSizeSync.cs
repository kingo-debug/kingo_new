using TMPro;
using UnityEngine;

public class TextSizeSync : MonoBehaviour
{
    private TextMeshProUGUI Text;
    RectTransform Rect;

    private void OnEnable()
    {
        Rect = GetComponent<RectTransform>();
        Text = GetComponent<TextMeshProUGUI>();
        SyncTextSize();
     }
    public void SyncTextSize()
    {
        Rect.sizeDelta = Text.GetPreferredValues();
    }
}
