using UnityEngine;

public class ArmorUICheck : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup group;

 public void ShowShieldBar()
    {
        group.alpha = 1;
    }


    public void HideShieldBar()
    {
        group.alpha = 0;
    }
}

