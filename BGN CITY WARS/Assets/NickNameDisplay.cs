using UnityEngine;
using TMPro;
public class NickNameDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PhotonSerializerBGN data;
    [SerializeField]
    TextMeshProUGUI text;
    void Start()
    {
        text.text = data.PlayerNickName;
    }

}
