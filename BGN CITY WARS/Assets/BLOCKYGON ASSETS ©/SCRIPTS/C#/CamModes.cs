using UnityEngine;

public class CamModes : MonoBehaviour
{
    public Camera Camera;
    [SerializeField]
    private camera2 camcontroller;
    public PlayerActionsVar vars;
    //CAM MODES
    public float FMC = 30f;

    [SerializeField]
    private float CombatDistance = 2f;





    [SerializeField]
    private float FreeDistance = 3f;




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

    
    
    private void Awake()
    {
       Camera = Camera.main;
        scopemanager = GetComponent<ScopingManager>();
    }
    private void Start()
    {
        vars = GetComponent<PlayerActionsVar>();

    }
    void Update()

    {
      
        if (!vars.Sprinting)
        {
        if (vars.Combat) // combat mode//

            {

                camcontroller.zOffset = Mathf.Lerp(camcontroller.zOffset, CombatDistance, Time.deltaTime * smoothness);
         
            }

            if (vars.IsAiming && ! scopemanager.CanScope) // hipsfire mode//
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, AMC, ref currentspeedAMC, Time.deltaTime * smoothnessAMC);
        }

        else if ( vars.IsAiming && scopemanager.CanScope) // scope aiming mode//
        {
            Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, SCOPE, ref currentspeedFMC, Time.deltaTime * smoothness);

        }
        if(!vars.Combat)  // free mode //
            {
                Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, FMC, ref currentspeedFMC, Time.deltaTime * smoothness);
                
                camcontroller.zOffset = Mathf.Lerp(camcontroller.zOffset, FreeDistance, Time.deltaTime * smoothness);



            }

        }
        else SprintfOV();

}
    void SprintfOV()
    {
     if(vars.Sprinting)
    {

     Camera.fieldOfView = Mathf.SmoothDamp(Camera.fieldOfView, SprintingFOV, ref currentspeedCMC, Time.deltaTime * SprintDamp);
     
    }   

    }





}
