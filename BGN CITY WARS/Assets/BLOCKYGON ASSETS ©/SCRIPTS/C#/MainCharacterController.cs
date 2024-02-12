using UnityEngine;
using Photon.Pun;
using System.Collections;
using ControlFreak2;
using RootMotion.FinalIK;




public class MainCharacterController : MonoBehaviour
{
    #region Variables
    [Space(10)]
    [Header("ControollerMode")]
    [Space(3)]
    public bool Combatmode;
    [SerializeField]
    private float CombatCoolDown;
    [Space(10)]
    [Header("CombatMode")]
    [Space(3)]
    [SerializeField]
    private float RotateTowardsSpeed;
    public bool ISAiming = false;
    [SerializeField]
    private float CMMoveThreshold;
    public float CMSpeed;
    [Space(10)]
    [Header("FreeMode")]
    [Space(3)]
    [SerializeField]
    private float FMMoveThreshold;
    public float FMSpeed;
    public float MoveSmoothness;
    private float Lerp;
    [SerializeField]
    private float FreeRotationSpeed;
    private Quaternion freeRotation;
    public float turnSpeed = 10f;
    //misc
    private PhotonView PV;
    private ControlFreak2.TouchJoystick joystick;
    private CharacterController CharController;
    private Transform MainCamera;
    private Vector3 targetDirection;
    [Space(3)]
    [Header("JumpingSystem")]
    public bool isGrounded;
    [SerializeField]
    private float jumpHeight = 5f;
    public float gravity = 9.8f;
    private Vector3 velocity;
    private Animator animator;
    private SpeedCheck speedcheck;
    private float SyncSpeed = 10f;
    private LookAtIK lookik;
    private AimIK aimik;
    private PlayerActionsVar actionsVar;




    #endregion
    void Start()
    {
        
        joystick = GameObject.FindWithTag("JoyStick").GetComponent<TouchJoystick>();

        animator = GetComponent<Animator>();

        PV = GetComponent<PhotonView>();

        CharController = GetComponent<CharacterController>();

        MainCamera = Camera.main.transform;

        speedcheck = GetComponent<SpeedCheck>();
        lookik = GetComponent<LookAtIK>();
        aimik = GetComponent<AimIK>();
        actionsVar = GetComponent<PlayerActionsVar>();
        animator.SetBool("IS AIMING", false);
        if (!PV.IsMine)
        {
            this.enabled = false; ;
        }

        FreeMode();



    }



    // Update is called once per frame
    void Update()
    {
        isGrounded = CharController.isGrounded;
        #region Animator Parameters.
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("combat", Combatmode);

        if (actionsVar.Fired || ISAiming)
        {
            Combatmode = true;

        }

        if (Combatmode && !actionsVar.Fired && !ISAiming && !animator.GetBool("FIRE INPUT"))
        {
            StartCoroutine(ResetCombatMode());

        }


        if (float.IsNaN(animator.GetFloat("PlayerVelocity")))
        {
            animator.SetFloat("PlayerVelocity", 0f);
            animator.SetFloat("inputx", 0f);
            animator.SetFloat("inputY", 0f);
            animator.SetFloat("inputMagnitude", 0f);

        }
        else
        {
            animator.SetFloat("PlayerVelocity", speedcheck.speed, 1, Time.deltaTime * 9);
            animator.SetFloat("inputx", joystick.GetVector().x * SyncSpeed, 0.5f, Time.deltaTime * 5);
            animator.SetFloat("inputY", joystick.GetVector().y * SyncSpeed, 0.5f, Time.deltaTime * 5);
            animator.SetFloat("inputMagnitude", new Vector2(animator.GetFloat("inputx"), animator.GetFloat("inputY")).magnitude);

        }

        #endregion

        if (Combatmode)
        {
            CombatMode();

        }
        else
        {
            FreeMode();
        }




        Jump();
        Aim();
        Shoot();
    }

    void CombatMode()
    {
        //Combat mode features

        #region Strafe Move
        Vector3 Strafemove = transform.rotation * new Vector3(joystick.GetVector().x * CMSpeed, 0, joystick.GetVector().y * CMSpeed) * Time.deltaTime;
        CharController.Move(Strafemove);
        #endregion
        #region RotateChar
        Vector3 Rotation = new Vector3(MainCamera.forward.x, 0, MainCamera.forward.z);

        Vector3 SmoothRotation = Vector3.Lerp(transform.forward, Rotation, Time.deltaTime * RotateTowardsSpeed);

        transform.forward = (SmoothRotation);
        #endregion

    }

    void FreeMode()
    {

        #region Free Movement
        if (PV.IsMine && joystick.GetVector().magnitude > FMMoveThreshold)

        {
            // Get the joystick input vector
            Vector3 joystickInput = joystick.GetVector();
            if (Lerp < FMSpeed)
            {
                Lerp = Mathf.Clamp(Lerp += MoveSmoothness * Time.deltaTime, 0, FMSpeed);
            }




            // Create a movement vector based on the joystick input
            Vector3 movement = transform.rotation * new Vector3(0, 0, Mathf.Abs(joystickInput.magnitude * Lerp * Time.deltaTime));

            // Apply the movement to the character controller
            CharController.Move(movement);
        }
        else
        {
            if (Lerp > 0)
            {
                Lerp = Mathf.Clamp(Lerp -= MoveSmoothness * 0.5f * Time.deltaTime, 0, FMSpeed);
            }
        }

        #endregion

        #region Free Rotate
        if (joystick.GetVector() != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * FreeRotationSpeed * Time.deltaTime);
        }

        FreeRotationSpeed = 1f;
        var forward = MainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        //get the right-facing direction of the referenceTransform
        var right = MainCamera.transform.TransformDirection(Vector3.right);

        // determine the direction the player will face based on input and the referenceTransform's right and forward directions
        targetDirection = joystick.GetVector().x * right + joystick.GetVector().y * forward;

        #endregion


    }

    IEnumerator ResetCombatMode()
    {
        yield return new WaitForSeconds(CombatCoolDown);

        if (Combatmode && !actionsVar.Fired && !ISAiming && !animator.GetBool("FIRE INPUT"))
        {
            Combatmode = false;
        }




    }


    void Jump()
    {
        if(PV.IsMine)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Ensure you are grounded to avoid gravity accumulation
            }

            if (ControlFreak2.CF2Input.GetKey(KeyCode.Space) && isGrounded)
            {
                // Calculate the jump velocity based on jump height
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            }

            // Apply gravity to pull the character down
            velocity.y += gravity * Time.deltaTime;

            // Move the character
            CharController.Move(velocity * Time.deltaTime);
        }


    }


    void Aim()
    {
        if(PV.IsMine)
        {
            if (ControlFreak2.CF2Input.GetMouseButtonDown(1) && !actionsVar.IsReloading && actionsVar.Weapontype > 0)
            {

                if (ISAiming)
                {
                    ISAiming = false;
                    animator.SetBool("IS AIMING", false);
                    aimik.GetIKSolver().SetIKPositionWeight(0);
                    lookik.GetIKSolver().SetIKPositionWeight(0);
                    actionsVar.IsAiming = false;


                }
                else
                {
                    ISAiming = true;
                    animator.SetBool("IS AIMING", true);
                    aimik.GetIKSolver().SetIKPositionWeight(.8f);
                    lookik.GetIKSolver().SetIKPositionWeight(0.8f);
                    actionsVar.IsAiming = true;

                }

            }
            else if (actionsVar.IsReloading)
            {
                ISAiming = false;
                animator.SetBool("IS AIMING", false);
                aimik.GetIKSolver().SetIKPositionWeight(0);
                lookik.GetIKSolver().SetIKPositionWeight(0);
                actionsVar.IsAiming = false;

            }
        }

    }

    void Shoot()
    {
      if (PV.IsMine)
        {
            if (ControlFreak2.CF2Input.GetMouseButton(0) && !actionsVar.IsReloading)
            {
                aimik.GetIKSolver().SetIKPositionWeight(.8f);
                lookik.GetIKSolver().SetIKPositionWeight(0.8f);
            }
            else if (!ISAiming)
            {
                aimik.GetIKSolver().SetIKPositionWeight(0);
                lookik.GetIKSolver().SetIKPositionWeight(0);
            }
        }
    }

}






