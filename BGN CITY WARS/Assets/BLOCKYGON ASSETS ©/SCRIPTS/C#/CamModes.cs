using UnityEngine;

public class CamModes : MonoBehaviour
{
    public Camera Camera;
    [SerializeField]
    private MainCameraController camcontroller;
    public PlayerActionsVar vars;
    //CAM MODES
    public float FMC = 30f;

    [SerializeField]
    private float CombatDistance = 2f;

    public float AMC = 21;
    public float SCOPE = 15;
    public float SprintingFOV = 40f;
    public float SprintDamp = 40f;
    public float smoothness = 40f;
    public float smoothnessAMC = 20f;
    private float currentspeedFMC;
    private  float currentspeedCMC;
    private float currentspeedAMC;
    private ScopingManager scopemanager;
    private bool previousCombatState;


    private void Awake()
    {
       Camera = Camera.main;
        scopemanager = GetComponent<ScopingManager>();
    }
    private void Start()
    {
        vars = GetComponent<PlayerActionsVar>();
        previousCombatState = vars.Combat;

    }
    void Update()

    {          
    if (vars.IsAiming && ! scopemanager.CanScope) // hipsfire mode//
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, AMC, ref currentspeedAMC, Time.deltaTime * smoothnessAMC);
        }

        else if ( vars.IsAiming && scopemanager.CanScope) // scope aiming mode//
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, SCOPE, ref currentspeedFMC, Time.deltaTime * smoothness);
        }
        if(!vars.IsAiming)  // free mode //
            {
                Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, FMC, ref currentspeedFMC, Time.deltaTime * smoothness); 
            }

        // Check if the combat state has changed
        if (vars.Combat != previousCombatState)
        {
            if (vars.Combat)
            {
                camcontroller.CustomCull();
                Invoke("ReCheckCombat", .002f);
            }

               // Update the previousCombatState to the current state
            previousCombatState = vars.Combat;
        }
    }
    void ReCheckCombat()
    {
        previousCombatState = vars.Combat ? false : true;
    }
    void SprintfOV()
    {
     if(vars.Sprinting)
    {

     Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, SprintingFOV, ref currentspeedCMC, Time.deltaTime * SprintDamp);
     
    }   

    }




}
