using UnityEngine;
using Photon.Pun;
using System.Collections;
using ControlFreak2;
using RootMotion.FinalIK;




public class MainCharacterController : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [Space(10)]
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 move;
    public float speed = 6.0f;
    [SerializeField]
    private Transform PlayerBody;
    [Header("ControllerMode")]
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
    [Space(10)]
    [Header("FreeMode")]
    [Space(3)]
    public float AirMoveSmoothness;
    [SerializeField]
    private float accelerationRate = 10f;
    [SerializeField]
    private float decelerationRate = 10f;

    [SerializeField]
    private float FreeRotationSpeed = 0.15f;
    public float turnSpeed = 10f;
    //misc
    private PhotonView PV;
    private ControlFreak2.TouchJoystick joystick;
    private CharacterController CharController;
    private Transform MainCamera;

    [Space(3)]
    [Header("JumpingSystem")]
    public bool isGrounded;
    public float airControlFactor = 0.5f;
    public float maxAirSpeed = 3.0f; // Max speed while airborne
    [SerializeField]
    private float AirRotationSpeed = 0.08f;
    // Define this variable at the class level
    private float nextJumpTime = 0f;
    public float jumpCooldown = 0.5f; // Set this to the desired cooldown time

    [SerializeField]
    private float jumpHeight = 5f;
    public bool Jumping = false;
    [SerializeField]
    private float JumpTime = .32f;
    public float gravity = 9.8f;


    [Space(3)]
    [Header("Animation")]
    private Vector3 velocity;
    private Animator animator;
    private SpeedCheck speedcheck;
    [SerializeField]
    private float SyncSpeed = 10f;
    private LookAtIK lookik;
    private AimIK aimik;
    private PlayerActionsVar actionsVar;
    private WeaponStatus weaponstatus;
    private SwimPlayerControl swimcontrols;
    private JetPackManager jpmanager;

    public Transform cameraTransform; // Reference to the camera's transform

    [Space(3)]
    [Header("Roll System")]
    public bool Rolling;

    [Space(3)]
    [Header("IK System")]
    public float DefaultlookIK = 0.3f;

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
        swimcontrols = GetComponent<SwimPlayerControl>();
        weaponstatus = GetComponent<WeaponStatus>();
        jpmanager = GetComponent<JetPackManager>();
        if (!PV.IsMine)
        {
            this.enabled = false; ;
        }
    }



    // Update is called once per frame
    void Update()
    {  
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
            animator.SetFloat("PlayerVelocity", speedcheck.horizontalSpeed, 1, Time.deltaTime * 9);
            animator.SetFloat("inputx", joystick.GetVector().x * SyncSpeed, 0.5f, Time.deltaTime * 1.15f);
            animator.SetFloat("inputY", joystick.GetVector().y * SyncSpeed, 0.5f, Time.deltaTime * 1.15f);
            animator.SetFloat("inputMagnitude", new Vector2(animator.GetFloat("inputx"), animator.GetFloat("inputY")).magnitude);
        }
        #endregion
        #region Input setup
        float moveHorizontal = ControlFreak2.CF2Input.GetAxis("Horizontal");
        float moveVertical = ControlFreak2.CF2Input.GetAxis("Vertical");
        #endregion
        isGrounded = CharController.isGrounded;
        actionsVar.Combat = Combatmode;
        #region Gravity Apply
        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // Ensure the player stays grounded
        }

        #endregion


        if (Combatmode)
        {
            CombatModeMovement(moveHorizontal, moveVertical);
            RotateToCameraDirection();
        }
        else
        {
            FreeModeMovement(moveHorizontal, moveVertical);
        }

        moveDirection.y += gravity * Time.deltaTime;
        CharController.Move(moveDirection * Time.deltaTime);

        // Create  Movement
        move = transform.right * moveHorizontal + transform.forward * moveVertical;

        if (jpmanager.JetPackActive && ControlFreak2.CF2Input.GetKey(KeyCode.Space) && !Jumping)
        {
            moveDirection.y = -0f;
        }


        #region IK 
        if (!ISAiming && aimik.GetIKSolver().IKPositionWeight > 0)
        {
            StopAim();
        }
        #endregion
        #region RollingCheck
        if (Rolling)
        {
            Roll();
        }
        if (!Rolling && animator.GetLayerWeight(7) > 0)
        {
            ResetRoll();
        }
        #endregion

        Aim();
        Shoot();
    }




    void CombatModeMovement(float moveHorizontal, float moveVertical)
    {
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
        move = Vector3.ClampMagnitude(move, 1f); // Normalize the vector to maintain consistent speed

        if (isGrounded)
        {
            moveDirection = move * speed;

            // Check if the player is allowed to jump
            if (ControlFreak2.CF2Input.GetKey(KeyCode.Space) && Time.time >= nextJumpTime)
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Jumping = true;
                StartCoroutine(Resetjump());

                // Set the next allowed jump time
                nextJumpTime = Time.time + jumpCooldown;
            }
        }
        else
        {
            // Airborne control
            moveDirection.x += move.x * speed * airControlFactor * Time.deltaTime;
            moveDirection.z += move.z * speed * airControlFactor * Time.deltaTime;

            // Clamp the horizontal speed
            Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
            if (horizontalVelocity.magnitude > maxAirSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * maxAirSpeed;
                moveDirection.x = horizontalVelocity.x;
                moveDirection.z = horizontalVelocity.z;
            }
        }
    }

    void RotateToCameraDirection()
    {
        // Only rotate around the Y-axis to follow the camera's direction
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Keep rotation on the horizontal plane
        if (cameraForward.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotateTowardsSpeed);
        }
    }

    void FreeModeMovement(float moveHorizontal, float moveVertical)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 move = cameraRight * moveHorizontal + cameraForward * moveVertical;
        move.Normalize();

        if (isGrounded)
        {
            // Calculate target move direction based on input
            Vector3 targetMoveDirection = move * speed;

            // Apply acceleration when there is input
            moveDirection.x = Mathf.Lerp(moveDirection.x, targetMoveDirection.x, Time.deltaTime * accelerationRate);
            moveDirection.z = Mathf.Lerp(moveDirection.z, targetMoveDirection.z, Time.deltaTime * accelerationRate);

            // Check if the player is allowed to jump
            if (ControlFreak2.CF2Input.GetKey(KeyCode.Space) && Time.time >= nextJumpTime)
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Jumping = true;
                StartCoroutine(Resetjump());

                // Set the next allowed jump time
                nextJumpTime = Time.time + jumpCooldown;
            }

            // Apply deceleration when no input is detected
            if (moveHorizontal == 0 && moveVertical == 0)
            {
                moveDirection.x = Mathf.Lerp(moveDirection.x, 0, Time.deltaTime * decelerationRate);
                moveDirection.z = Mathf.Lerp(moveDirection.z, 0, Time.deltaTime * decelerationRate);
            }
        }
        else
        {
            // Airborne control
            moveDirection.x += move.x * speed * airControlFactor * Time.deltaTime;
            moveDirection.z += move.z * speed * airControlFactor * Time.deltaTime;

            Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
            if (horizontalVelocity.magnitude > maxAirSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * maxAirSpeed;
                moveDirection.x = horizontalVelocity.x;
                moveDirection.z = horizontalVelocity.z;
            }
        }

        #region Free Rotate
        if (move != Vector3.zero) // rotate free
        {
            float rotationspeed = (isGrounded) ? FreeRotationSpeed : AirRotationSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), rotationspeed);
        }
        #endregion
    }






    void CombatMode()
    {
        //Combat mode features
        if (PV.IsMine&& CharController.isGrounded)
        {
            // Get the joystick input vector
            Vector3 joystickInput = joystick.GetVector();                
            #region RotateChar
            Vector3 Rotation = new Vector3(MainCamera.forward.x, 0, MainCamera.forward.z);

            Vector3 SmoothRotation = Vector3.Lerp(transform.forward, Rotation, Time.deltaTime * RotateTowardsSpeed);

            transform.forward = (SmoothRotation);
            #endregion
            actionsVar.Combat = Combatmode;
        }       
    }



    public void AirBornMode()
    {
        // Airborne control
        moveDirection.x += move.x * speed * airControlFactor * Time.deltaTime;
        moveDirection.z += move.z * speed * airControlFactor * Time.deltaTime;
        if (jpmanager.Accelerating)
                {
                    #region RotateChar
                    Vector3 Rotation = new Vector3(MainCamera.forward.x, 0, MainCamera.forward.z);

                    Vector3 SmoothRotation = Vector3.Lerp(transform.forward, Rotation, Time.deltaTime * RotateTowardsSpeed);

                    transform.forward = (SmoothRotation);
                    #endregion
                }             
    }
    IEnumerator ResetCombatMode()
    {
        yield return new WaitForSeconds(CombatCoolDown);

        if (Combatmode && !actionsVar.Fired && !ISAiming && !animator.GetBool("FIRE INPUT"))
        {
            Combatmode = false;
        }




    }
    IEnumerator Resetjump()
    {
        yield return new WaitForSeconds(JumpTime);
        Jumping = false;

    }

    void Jump()
    {
   if (ControlFreak2.CF2Input.GetKey(KeyCode.Space) && isGrounded && !Rolling)
     {        
                Jumping = true;
                StartCoroutine(Resetjump());            
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
     }
                 

        if (jpmanager.JetPackActive && ControlFreak2.CF2Input.GetKey(KeyCode.Space) && !Jumping)
        {
            velocity.y = -0f;
        }

    }





    void Aim()
    {
        if (PV.IsMine)
        {
            if (ControlFreak2.CF2Input.GetMouseButtonDown(1)) // first condition
                if (!actionsVar.IsReloading && actionsVar.Weapontype > 0 && !swimcontrols.Swiming && !Rolling)
                {
                    if (ISAiming)
                    {
                        ISAiming = false;
                        //   animator.SetBool("IS AIMING", false);
                        //     weaponstatus.CurrentWeapon.GetComponent<WeaponRecoil>().PlayerAiming = false;
                        //  aimik.GetIKSolver().SetIKPositionWeight(0);
                        // lookik.GetIKSolver().SetIKPositionWeight(0);
                        //  actionsVar.IsAiming = false;

                        if (weaponstatus.CurrentWeapon.GetComponent<WeaponType>().Scope)
                        {
                            GetComponent<ScopingManager>().ScopeOff();
                        }

                    }
                    else
                    {
                        ISAiming = true;
                        weaponstatus.CurrentWeapon.GetComponent<WeaponRecoil>().PlayerAiming = true;
                        animator.SetBool("IS AIMING", true);
                        aimik.GetIKSolver().SetIKPositionWeight(.8f);
                        lookik.GetIKSolver().SetIKPositionWeight(0.8f);
                        actionsVar.IsAiming = true;
                        //scope check
                        if (weaponstatus.CurrentWeapon.GetComponent<WeaponType>().Scope)
                        {
                            GetComponent<ScopingManager>().ScopeOn();
                        }


                    }
                }





                else if (actionsVar.IsReloading)
                {
                    ISAiming = false;
                    //    weaponstatus.CurrentWeapon.GetComponent<WeaponRecoil>().PlayerAiming = false;
                    //  animator.SetBool("IS AIMING", false);
                    //   aimik.GetIKSolver().SetIKPositionWeight(0);
                    //   lookik.GetIKSolver().SetIKPositionWeight(0);
                    //      actionsVar.IsAiming = false;

                    if (weaponstatus.CurrentWeapon.GetComponent<WeaponType>().Scope)
                    {
                        GetComponent<ScopingManager>().ScopeOff();
                    }




                }
        }

    }
    public void StopAim()
    {
        ISAiming = false;
        if (weaponstatus.CurrentWeapon.GetComponent<WeaponRecoil>() != null)
        {
            weaponstatus.CurrentWeapon.GetComponent<WeaponRecoil>().PlayerAiming = false;
        }
        animator.SetBool("IS AIMING", false);
        aimik.GetIKSolver().SetIKPositionWeight(Mathf.Lerp(aimik.GetIKSolver().IKPositionWeight, -0.01f, Time.deltaTime * 1.15f));
        lookik.GetIKSolver().SetIKPositionWeight(Mathf.Lerp(lookik.GetIKSolver().IKPositionWeight, DefaultlookIK, Time.deltaTime * 1.15f));
        actionsVar.IsAiming = false;
        if (weaponstatus.CurrentWeapon.GetComponent<WeaponType>().Scope)
        {
            GetComponent<ScopingManager>().ScopeOff();
        }

    }
    void Shoot()
    {
        if (PV.IsMine)
        {
            if (ControlFreak2.CF2Input.GetMouseButton(0) && !actionsVar.IsReloading && !weaponstatus.NoAmmo && !Rolling)
            {
                aimik.GetIKSolver().SetIKPositionWeight(.8f);
                lookik.GetIKSolver().SetIKPositionWeight(0.8f);
            }
            else if (!ISAiming)
            {
                //   aimik.GetIKSolver().SetIKPositionWeight(0);
                //  lookik.GetIKSolver().SetIKPositionWeight(0);
                StopAim();
            }
        }
    }
    public void Roll()
    {
        if (isGrounded)
        {
            Rolling = true;
            animator.SetLayerWeight(7, Mathf.Lerp(animator.GetLayerWeight(7), 1.1f, Time.deltaTime * 8));
            animator.SetBool("ROLL", true);
            StopAim();
            // FreeMode();
            Combatmode = false;
        }

    }
    public void ResetRoll()
    {
        Rolling = false;
        animator.SetLayerWeight(7, Mathf.Lerp(animator.GetLayerWeight(7), -0.01f, Time.deltaTime * 7));
        animator.SetBool("ROLL", false);
    }
}


