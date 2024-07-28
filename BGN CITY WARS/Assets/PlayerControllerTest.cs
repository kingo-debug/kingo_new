using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float airControlFactor = 0.5f;
    public bool isCombatMode = true; // Toggle between combat and free mode

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded;
    public Transform cameraTransform; // Reference to the camera's transform

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f; // Ensure the player stays grounded
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (isCombatMode)
        {
            CombatModeMovement(moveHorizontal, moveVertical);
        }
        else
        {
            FreeModeMovement(moveHorizontal, moveVertical);
        }

        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void CombatModeMovement(float moveHorizontal, float moveVertical)
    {
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        if (isGrounded)
        {
            moveDirection = move * speed;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            // Airborne control
            moveDirection.x += move.x * speed * airControlFactor * Time.deltaTime;
            moveDirection.z += move.z * speed * airControlFactor * Time.deltaTime;
        }
    }

    void FreeModeMovement(float moveHorizontal, float moveVertical)
    {
        // Calculate the move direction relative to the camera
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0f; // Keep the movement in the horizontal plane
        cameraRight.y = 0f; // Keep the movement in the horizontal plane

        Vector3 move = cameraRight * moveHorizontal + cameraForward * moveVertical;
        move.Normalize(); // Normalize the movement vector to maintain consistent speed

        if (isGrounded)
        {
            moveDirection = move * speed;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            // Airborne control
            moveDirection.x += move.x * speed * airControlFactor * Time.deltaTime;
            moveDirection.z += move.z * speed * airControlFactor * Time.deltaTime;
        }

        // Rotate player to face the move direction
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15f);
        }
    }
}
