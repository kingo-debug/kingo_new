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
    [SerializeField]private float DistanceFromTarget;
    private float currentRight;
    private float currentX = 0.0f; // Current horizontal rotation
    private float currentY = 0.0f; // Current vertical rotation

    [Header("Culling")]
    public LayerMask CullMask;
    public float TotalCullAmount;
    public float CullRadius = 1f;
    public float CullRadius2 = 0.25f;
    public float CullRadius2Offset = 1f;
    public float CulledDistance = 2f; // Distance from the player
    public float CulledDistance2 = 0.5f; // Distance from the player
    public float RightDistanceCull = 0.2f; // Distance from the player
    public bool FirstCulled = false;
    public bool SecondCulled = false;
    public bool BackupSphereActive = false;
    public float BackupSphereSize = 1f;
    public float BackupSphereDistance = 2;
    //second backup
    public bool SecondBackupSphereActive = false;
    public float SecondBackupSphereSize = 0.5f;
    public float SecondBackupSphereDistance = 1.2f;

    void Start()
    {
        // Initialize the current rotation based on the camera's initial rotation
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;
        currentDistance = DefaultDistance; // Initialize currentDistance with DefaultDistance
        currentRight = RightDistance;
        TotalCullAmount = DefaultDistance;
        RefreshSensSetting();
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
      
      // Check for collision with the larger sphere
        if ( Physics.CheckSphere(transform.position, CullRadius, CullMask) ) 
        {
            if(!FirstCulled)
            {
                FirstCulled = true;
                TotalCullAmount += CulledDistance; // adjust camera distance at first sphere ball
                Invoke("UpdateCull", 3f);
            }
        }
        // Check for collision with the smaller sphere
        if (Physics.CheckSphere(transform.position - transform.forward * CullRadius2Offset, CullRadius2, CullMask))
        {
            if (!SecondCulled)
            {
                SecondCulled = true;
                TotalCullAmount +=CulledDistance2; // adjust camera distance after second sphere ball
                RightDistance -= RightDistanceCull; // adjust camera right distance  after second sphere ball
                Invoke("UpdateCull2", 0.5F);
            }


        }

        // Gradually interpolate the current distance to the target distance
        currentDistance = Mathf.Lerp(currentDistance, TotalCullAmount, Time.deltaTime * DampSmoothness);

        currentRight = Mathf.Lerp(currentRight, RightDistance, Time.deltaTime * DampSmoothness);
        // Calculate the new rotation
        Quaternion targetRotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPosition = targetRotation * new Vector3(currentRight, HeightDistance, -currentDistance) + player.position;


        Vector3 convertdistance = transform.position - player.position;
        DistanceFromTarget = convertdistance.magnitude;
        // Smoothly interpolate towards the target rotation and position

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSmooth); // set up look at
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * SpeedSmooth); // set up follow
    }
    void  BackUpCull()
    {
        if (Physics.CheckSphere(transform.position - transform.forward * BackupSphereDistance, BackupSphereSize, CullMask)) // FirstBackup
        {
            BackupSphereActive = true;
        }
        else
        {
            BackupSphereActive = false;
        }



        if (Physics.CheckSphere(transform.position - transform.forward * SecondBackupSphereDistance, SecondBackupSphereSize, CullMask)) //Second Backup
        {
            SecondBackupSphereActive = true;
        }
        else
        {
            SecondBackupSphereActive = false;
        }
    }
     void UpdateCull()
    {
       if (!BackupSphereActive && !SecondBackupSphereActive)
        {
            FirstCulled = false;
            TotalCullAmount -= CulledDistance;
        }
        else Invoke("UpdateCull", 3f);

    }
    void UpdateCull2()
    {
        if (!SecondBackupSphereActive)
        {
            SecondCulled = false;
            TotalCullAmount -=CulledDistance2; // remove extra added culls at exit
            RightDistance += RightDistanceCull; // rstore camera right distance  after second sphere ball
        }
        else Invoke("UpdateCull2", 0.5f);

    }

    private void OnDrawGizmosSelected()
    {
        // Set color to red for culls
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, CullRadius); // show cull
        Gizmos.DrawSphere(transform.position - transform.forward * CullRadius2Offset, CullRadius2); // show cull 2

        // Set color to green for backups
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position - transform.forward * BackupSphereDistance, BackupSphereSize); // show cull backup
        Gizmos.DrawSphere(transform.position - transform.forward * SecondBackupSphereDistance, SecondBackupSphereSize); // show cull backup
    }
    public void CustomCull()
    {
        if (!FirstCulled)
        {
            FirstCulled = true;
            TotalCullAmount += CulledDistance; // adjust camera distance at first sphere ball
            Invoke("UpdateCull", 3f);
        }
    }
    public void RefreshSensSetting()
    {
        mouseSensitivity = ES3.Load<float>("GeneralSense");
    }

}


