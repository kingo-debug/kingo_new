using UnityEngine;

public class SpeedCheck : MonoBehaviour
{
    public CharacterController characterController;
    private JetPackManager JPmanager;
    private Animator animator;
    public float horizontalSpeed;
    public float fallSpeed;
    private Vector3 lastPosition;
    private float lastTime;

    private void Start()
    {
        // Initialize the last position and time
        lastPosition = characterController.transform.position;
        lastTime = Time.time;
        JPmanager = GetComponent<JetPackManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Calculate the speed based on position changes over time
        Vector3 currentPosition = characterController.transform.position;
        float currentTime = Time.time;
        float deltaTime = currentTime - lastTime;

        // Calculate the distance traveled
        Vector3 displacement = currentPosition - lastPosition;

        // Calculate the horizontal speed for X and Z components
        horizontalSpeed = new Vector3(displacement.x, 0, displacement.z).magnitude / deltaTime;

        // Calculate the fall speed for Y component (up and down)
        fallSpeed = displacement.y / deltaTime;

        // Update last position and time for the next frame
        lastPosition = currentPosition;
        lastTime = currentTime;

        // Check if the falling fast condition is met
        if (fallSpeed < -10 && !JPmanager.JetPackActive)
        {
            animator.SetBool("FALLING FAST", true);
        }
        else
        {
            animator.SetBool("FALLING FAST", false);
        }

        // Optionally, you can debug log the speeds if needed
        // Debug.Log("Horizontal Speed: " + horizontalSpeed);
        // Debug.Log("Fall Speed: " + fallSpeed);
    }


}
