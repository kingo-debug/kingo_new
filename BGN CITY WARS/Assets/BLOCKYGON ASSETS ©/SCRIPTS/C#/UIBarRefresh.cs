using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBarRefresh : MonoBehaviour

{
    [SerializeField]
    private TMPro.TextMeshProUGUI txt;
    [SerializeField]
    private Slider slider;
    private int Hp;

    private void Start()
    {
        UpdateHP(Hp);
    }
    public void UpdateHP(int HP)
    {
        Hp = HP;
        txt.text = HP.ToString();
        slider.value = HP;
        slider.maxValue = HP;

    }
}
