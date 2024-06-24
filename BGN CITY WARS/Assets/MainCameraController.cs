using UnityEngine;


public class MainCameraController : MonoBehaviour
{
    [Header("Distancing")]
    public Transform player; // The player transform to follow
    public float DefaultDistance = 5.0f; // Distance from the player
    public float RightDistance = 0f; // Distance from the player
    public float HeightDistance = 1f; // Distance from the player

    [Header("Controls")]
    public float mouseSensitivity = 2.0f; // Sensitivity of the mouse input
    public float yMinLimit = -20f; // Minimum vertical angle
    public float yMaxLimit = 80f; // Maximum vertical angle

    [Header("Smoothing")]
    public float RotationSmooth = 5f;
    public float SpeedSmooth = 5f;
    public float DampSmoothness = 0.1f; // Variable to control smoothness
    private float currentDistance;
    private float currentX = 0.0f; // Current horizontal rotation
    private float currentY = 0.0f; // Current vertical rotation

    [Header("Culling")]
    public LayerMask CullMask;
    public float TotalCullAmount;
    public float CullRadius = 1f;
    public float CullRadius2 = 0.25f;
    public float CulledDistance = 2f; // Distance from the player
    public float CulledDistance2 = 0.5f; // Distance from the player
    public float RightDistanceCull = 0.2f; // Distance from the player
    public bool DefaultCull = false;
    public bool FirstCulled = false;
    public bool SecondCulled = false;
    public bool BackupSphereActive = false;
    public float BackupSphereSize = 1f;
    public float BackupSphereDistance = 2;

    void Start()
    {
        // Initialize the current rotation based on the camera's initial rotation
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;
        currentDistance = DefaultDistance; // Initialize currentDistance with DefaultDistance
        TotalCullAmount = DefaultDistance;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Get mouse input
        currentX += ControlFreak2.CF2Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= ControlFreak2.CF2Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Clamp the vertical rotation
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

        CheckCulling();
        BackUpCull();
    }



void CheckCulling()
    {
        // Calculate the new distance based on collision checks
      
        float targetDistance = DefaultDistance;
        // Check for collision with the larger sphere
    
        if ( Physics.CheckSphere(transform.position, CullRadius, CullMask) ) 
        {
            if(!FirstCulled)
            {
                FirstCulled = true;
                DefaultCull = false;
                TotalCullAmount += CulledDistance; // adjust camera distance at first sphere ball
                Invoke("UpdateCull", 3f);
            }
        }
        // Check for collision with the smaller sphere
        if (Physics.CheckSphere(transform.position, CullRadius2, CullMask))
        {
            if (!SecondCulled)
            {
                SecondCulled = true;
                DefaultCull = false;
                TotalCullAmount +=CulledDistance2; // adjust camera distance after second sphere ball
                RightDistance -= RightDistanceCull; // adjust camera right distance  after second sphere ball
                Invoke("UpdateCull2", 3f);
            }
        }

        // Gradually interpolate the current distance to the target distance
        currentDistance = Mathf.Lerp(currentDistance, TotalCullAmount, Time.deltaTime * DampSmoothness);
        // Calculate the new rotation
        Quaternion targetRotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPosition = targetRotation * new Vector3(RightDistance, HeightDistance, -currentDistance) + player.position;

        // Smoothly interpolate towards the target rotation and position
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSmooth);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * SpeedSmooth);
    }
void  BackUpCull()
    {
        if (Physics.CheckSphere(transform.position - transform.forward * BackupSphereDistance, BackupSphereSize, CullMask))
        {
            BackupSphereActive = true;
        }
        else
        {
            BackupSphereActive = false;
        }
    }

    void UpdateCull()
    {
       if (!BackupSphereActive)
        {
            FirstCulled = false;
            TotalCullAmount -= CulledDistance;
        }
        else Invoke("UpdateCull", 3f);

    }
    void UpdateCull2()
    {
        if (!FirstCulled)
        {
            SecondCulled = false;
            TotalCullAmount -=CulledDistance2; // remove extra added culls at exit
            RightDistance += RightDistanceCull; // rstore camera right distance  after second sphere ball
        }
        else Invoke("UpdateCull2", 3f);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, CullRadius); // show cull
        Gizmos.DrawSphere(transform.position, CullRadius2); // show cull 2

        Gizmos.DrawSphere(transform.position - transform.forward * BackupSphereDistance, BackupSphereSize); // show cull backup
    }


}


