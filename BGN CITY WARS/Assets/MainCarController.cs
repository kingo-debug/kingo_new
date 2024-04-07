
using UnityEngine;

public class MainCarController : MonoBehaviour
{


    private Rigidbody rb;
    public bool grounded;
    [SerializeField]
    private float Acceleration;
    [SerializeField]
    private float Deacceleration;
    public bool Accelerating = false;
    public bool Reversing = false;
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    private float ReverseSpeed;

    private float Torque;
    [SerializeField]
    private float Gravity;
    [SerializeField]
    private float GroundDistance = 3.5f;
    [SerializeField]
    private Vector3 GroundOffset;
    [SerializeField]
    private Transform CenterOfMassTransform;
    private Vector3 lastGroundedPosition;

    [SerializeField]
    private float MaxSteer = 1;
    [SerializeField]
    private float MinSteerSpeed = 3f;
    [SerializeField]
    private float SlowSteer = 3f;
    [SerializeField]
    private float FastSteer = 3f;
    [SerializeField]
    private float SteerDirection = 1;
    [SerializeField]
    private float SteerSpeed = 3f;

    [Space(10)]
    [Header("Brakes")]
    [SerializeField]
  private float brakepower;
  public bool isBraking;




    public float MPH = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Set center of mass if the transform is assigned
        if (CenterOfMassTransform != null)
        {
            rb.centerOfMass = CenterOfMassTransform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        #region Inputs Setup
        if (Input.GetKey(KeyCode.W))
            Accelerating = true;          
            else Accelerating = false;
        if (Input.GetKey(KeyCode.S))
            Reversing = true;
            else Reversing = false;

            #endregion
            MPH = rb.velocity.magnitude * 2.237f; // Convert m/s to MPH
        grounded = IsGrounded();
        Torque = Mathf.Clamp(Torque, -5, MaxSpeed);
        MinSteerSpeed = Mathf.Clamp(Torque*2.5f, SlowSteer, FastSteer);
        // Inputs forward drive
        if (Accelerating&&grounded)
        {
            Torque +=Acceleration * Time.deltaTime;   //DRIVE FORWARD//
        }
        else if(!Reversing)
        {
            Deaccelerate();
        }

        if (Reversing && grounded)
        {
            Torque += ReverseSpeed * Time.deltaTime;   //DRIVE backwards//
        }
        else if (!Accelerating)
        {
            Deaccelerate();
        }
        if (Input.GetKey(KeyCode.A)&& grounded&& MPH>2)
        {      
        transform.Rotate(0,-SteerDirection* MinSteerSpeed * Time.deltaTime,0); //Steer Left //
        }

     else if (Input.GetKey(KeyCode.D) && grounded && MPH > 2)
        {
            transform.Rotate(0, SteerDirection * MinSteerSpeed * Time.deltaTime, 0);   // Steer Right //
        }

     if(Input.GetKey(KeyCode.Space) && grounded)   // braking// 
        {
            ApplyBrakes();
        } 

           Accelerate();
           Reverse();
           AddGravity();
           IsGrounded();

    }



    void Accelerate()
    {
      
            rb.AddForce(transform.forward * Torque, ForceMode.Acceleration);  
 
    }


    void Deaccelerate()
    {
        Torque = Mathf.Lerp(Torque, 0, Time.deltaTime * Deacceleration);
    }

    void AddGravity()
    {
        if(!grounded)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * Gravity, ForceMode.Acceleration);
        }
    }



    void ApplyBrakes()
    {
        isBraking = true;
        Torque -= brakepower * Time.deltaTime;
        Torque = Mathf.Clamp(Torque, 0, MaxSpeed);
    }

    void Reverse()
    {
        rb.AddForce(-transform.forward * Torque, ForceMode.Acceleration);
    }

    bool IsGrounded()
        {
            RaycastHit hit;
            
            return Physics.Raycast(transform.position + GroundOffset, -transform.up, out hit, GroundDistance);
        }

  
}


