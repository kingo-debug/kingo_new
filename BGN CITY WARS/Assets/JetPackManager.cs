using UnityEngine;
using System.Collections;

public class JetPackManager : MonoBehaviour
{
    private CharacterController Charcontroller;
    public bool JetPackActive = false;

    [SerializeField]
    private bool CanAccel = false;
    private MainCharacterController maincont;
    public float initialJetpackSpeed = 0.5f;
    public float maxJetpackSpeed = 5f;
    public float accelerationRate = 0.1f;

    [Space(20)]
    [Header("Fuel")]
    public float CurrentFuel;
    public float MaxFuel = 100f;
    public float ConsumptionSpeed = 1f;
    private float currentJetpackSpeed;

    [Space(20)]
    [Header("VFX")]
    [SerializeField]
    private GameObject AccerlateVFX;

    [SerializeField]
    private GameObject JetPackObject;

    private UIBarRefresh UIBar;

    private void Start()
    {
        Charcontroller = GetComponent<CharacterController>();
        maincont = GetComponent<MainCharacterController>();
        CurrentFuel = MaxFuel;
        UIBar = GameObject.Find("JETPACK BAR").GetComponent<UIBarRefresh>();
        UIBar.gameObject.SetActive(false);
        UIBar.UpdateHP(Mathf.RoundToInt(CurrentFuel));

    }
    private void Update()
    {

        if (ControlFreak2.CF2Input.GetKey(KeyCode.Space) && !maincont.Jumping && JetPackActive&&!Charcontroller.isGrounded&& CurrentFuel >0)
        {
            AccelerateJP();
            if (!AccerlateVFX.activeSelf)
            AccerlateVFX.SetActive(true);
        }
        else
        {
            currentJetpackSpeed = Mathf.Clamp(currentJetpackSpeed - accelerationRate * Time.deltaTime, initialJetpackSpeed, maxJetpackSpeed);
            if (AccerlateVFX.activeSelf)
            AccerlateVFX.SetActive(false);
 
        }
    

    }

  void AccelerateJP()
    {
        // Gradually increase the jetpack speed
        currentJetpackSpeed = Mathf.Clamp(currentJetpackSpeed + accelerationRate * Time.deltaTime, initialJetpackSpeed, maxJetpackSpeed);

        // Move the character controller upwards using the jetpack speed
        Charcontroller.Move(Vector3.up * Time.deltaTime * currentJetpackSpeed);

        #region Fuel
        CurrentFuel = Mathf.Clamp(CurrentFuel - ConsumptionSpeed * Time.deltaTime, 0, MaxFuel);
        #endregion
        if (CurrentFuel<0.1)
        {
            DisableJP();
        }

        #region UI Bar
        UIBar.UpdateHP(Mathf.RoundToInt(CurrentFuel));
        #endregion
    }

    public void RestoreJetpackFuel()
    {
        JetPackActive = true;
        CurrentFuel = MaxFuel;
        JetPackObject.SetActive(true);
        UIBar.gameObject.SetActive(true);
    }

    public void DisableJP()
    {
        JetPackActive = false;
        CurrentFuel = 0;
        JetPackObject.SetActive(false);
        UIBar.gameObject.SetActive(false);
    }
}
