using UnityEngine;
using Photon.Pun;

public class JetPackManager : MonoBehaviourPunCallbacks,IPunObservable
{
    private CharacterController Charcontroller;
    public bool JetPackActive = false;

    [SerializeField]
    private bool CanAccel = false;
    private MainCharacterController maincont;
    public float initialJetpackSpeed = 0.5f;
    public float maxJetpackSpeed = 5f;
    public float accelerationRate = 0.1f;
    public bool Accelerating;
    private PhotonView PV;

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
    [SerializeField]
    private UIBarRefresh UIBar;

    private FallDamage falldamage;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        Charcontroller = GetComponent<CharacterController>();
        maincont = GetComponent<MainCharacterController>();
        CurrentFuel = MaxFuel;
        UIBar.gameObject.SetActive(false);
        UIBar.UpdateHP(Mathf.RoundToInt(CurrentFuel));
        falldamage = GetComponent<FallDamage>();



    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) // Sync Accel for other players
    {
        if (stream.IsWriting && PV != null)
        {
            stream.SendNext(Accelerating); //Accelerating? this player
            stream.SendNext(JetPackActive); //JetPackActive? this player
        }

        else
        {
            Accelerating = (bool)stream.ReceiveNext(); // other player Accelerating?
            JetPackActive = (bool)stream.ReceiveNext(); // other player JetPackActive?
        }
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            if (ControlFreak2.CF2Input.GetKey(KeyCode.Space) && !maincont.Jumping && JetPackActive&&!Charcontroller.isGrounded&& CurrentFuel >0)
        {
   
                AccelerateJP();
                if (!AccerlateVFX.activeSelf)
                AccerlateVFX.SetActive(true);
                Accelerating = true;

            }
 
        else
        {
            Accelerating = false;
            currentJetpackSpeed = Mathf.Clamp(currentJetpackSpeed - accelerationRate * Time.deltaTime, initialJetpackSpeed, maxJetpackSpeed);
            if (AccerlateVFX.activeSelf)
            AccerlateVFX.SetActive(false);
        }
    }
        else if (AccerlateVFX)
        {
            JetPackObject.SetActive(JetPackActive);
            AccerlateVFX.SetActive(Accelerating);
        }
    }
    void AccelerateJP()
    {
        // Gradually increase the jetpack speed
        currentJetpackSpeed = Mathf.Clamp(currentJetpackSpeed + accelerationRate * Time.deltaTime, initialJetpackSpeed, maxJetpackSpeed);

        // Move the character controller upwards using the jetpack speed
        Charcontroller.Move(Vector3.up * Time.deltaTime * currentJetpackSpeed);
        falldamage.ResetFalling();

        #region Fuel
        CurrentFuel = Mathf.Clamp(CurrentFuel - ConsumptionSpeed * Time.deltaTime, 0, MaxFuel);
        #endregion
        if (CurrentFuel<0.1)
        {
            PV.RPC("DisableJP", RpcTarget.AllBuffered);
        }

        #region UI Bar
        UIBar.UpdateHP(Mathf.RoundToInt(CurrentFuel));
        #endregion
    }
    [PunRPC]
    public void RestoreJetpackFuel()
    {
        if(PV.IsMine)
        {
            JetPackActive = true;
            CurrentFuel = MaxFuel;
            JetPackObject.SetActive(true);
            UIBar.gameObject.SetActive(true);
        }
        else
        {
            JetPackObject.SetActive(true);
        }

    }
    [PunRPC]
    public void DisableJP()
    {
        if(PV)
        {
            JetPackActive = false;
            CurrentFuel = 0;
            JetPackObject.SetActive(false);
            if(UIBar!=null)
            {
                UIBar.gameObject.SetActive(false);
            }
     
        }
        else
        {
            JetPackObject.SetActive(false);
        }
    
    }

  
   

}
