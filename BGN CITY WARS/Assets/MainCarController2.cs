using UnityEngine;

public class MainCarController2 : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float braking = 10f;
    public float reverseSpeed = 5f;
    public float maxSteeringAngle = 30f;
    public Transform[] wheelModels;

    Rigidbody rb;
    float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input handling
        float input = Input.GetAxis("Vertical");
        float steering = Input.GetAxis("Horizontal");

        // Applying acceleration, braking, or reversing forces
        if (input > 0)
        {
            if (currentSpeed < maxSpeed)
            {
                rb.AddForce(transform.forward * acceleration * input);
                currentSpeed = Mathf.Min(currentSpeed + Time.deltaTime * acceleration, maxSpeed);
            }
        }
        else if (input < 0)
        {
            rb.AddForce(transform.forward * reverseSpeed * input);
            currentSpeed = Mathf.Max(currentSpeed + Time.deltaTime * reverseSpeed, -maxSpeed / 2f); // Half the maxSpeed for reversing
        }
        else
        {
            // Gradual deceleration when no input
            rb.AddForce(-rb.velocity.normalized * braking);
            currentSpeed = Mathf.Max(currentSpeed - Time.deltaTime * braking, 0f);
        }

        // Applying steering torque
        rb.angularVelocity = Vector3.zero; // Reset angular velocity
        rb.rotation *= Quaternion.Euler(Vector3.up * steering * maxSteeringAngle * Time.deltaTime * currentSpeed / maxSpeed);

        // Syncing wheel models rotation
        foreach (Transform wheel in wheelModels)
        {
            wheel.Rotate(Vector3.right, rb.velocity.magnitude * Time.deltaTime * 10f); // Adjust multiplier as needed for wheel rotation speed
        }
    }

    void FixedUpdate()
    {
        // Apply gravity
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);
    }
}
