using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimate : MonoBehaviour
{
    public float SizeUp = 1f;
    public float ResetTime = 0.2f;
    Vector2 DefaultScale;
   public Color DefaultColor;
    public Color PressedColor;
    private Image image;

    private void Start()
    {
        DefaultScale = transform.localScale;
        image = GetComponent<Image>();
       image.color = DefaultColor;

    }
    public void Clicked()
    {
     
        transform.localScale = DefaultScale + new Vector2(SizeUp, SizeUp);
        image.color = PressedColor;
        Invoke("Reset", ResetTime);
    }

    private void Reset()
    {
        transform.localScale = DefaultScale;
        image.color = DefaultColor;
    }
}
