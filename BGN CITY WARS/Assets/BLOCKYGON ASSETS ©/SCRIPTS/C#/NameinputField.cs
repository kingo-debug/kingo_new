using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NameinputField : MonoBehaviour
{
    public InputField yourInputField;

    [SerializeField]
    private string UserName;

    [SerializeField]
    private UIOverHeadManager UIOverhead;

    private void Start()
    {
        // Add a listener to the input field's value changed event
        yourInputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string value)
    {

        ES3.Save("PlayerName", value);    //its like  public- int -FirstTime = 0 but dont need to give type  
        UserName = ES3.Load<string>("PlayerName");

    }


    private void OnEnable()
    {
    Updatefield();

    }
    private void OnDisable()
    {

   Updatefield();
    }

    void Updatefield()
    {
        UserName = UserName = ES3.Load<string>("PlayerName");
        PhotonNetwork.NickName = UserName;
        yourInputField.text = UserName;
        UIOverhead.UpdateUINameDisplay();
    }
}
