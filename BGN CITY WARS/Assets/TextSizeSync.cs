using TMPro;
using UnityEngine;

public class TextSizeSync : MonoBehaviour
{
    private TextMeshPro Text;
    RectTransform Rect;

    private void OnEnable()
    {
        Rect = GetComponent<RectTransform>();
        Text = GetComponent<TextMeshPro>();
        SyncTextSize();
     }
    public void SyncTextSize()
    {
        Rect.sizeDelta = Text.GetPreferredValues();
    }
}
