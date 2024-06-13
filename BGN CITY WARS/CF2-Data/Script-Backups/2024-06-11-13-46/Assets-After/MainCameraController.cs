using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform target;                 // The target for the camera to follow
    public float heightOffset = 2.0f;        // Height offset of the camera
    public float rightOffset = 2.0f;         // Right offset of the camera
    public float smoothSpeed = 0.125f;       // Smooth follow speed
    public float maxDistance = 10.0f;        // Max distance from the target
    public float minDistance = 2.0f;         // Min distance for culling
    public float zoomCullingOffset = 1.0f;   // Offset to apply when culling
    public LayerMask cullingLayer;           // Layer mask for culling objects

    public float mouseXSensitivity = 2.0f;   // Mouse X sensitivity
    public float mouseYSensitivity = 2.0f;   // Mouse Y sensitivity

    private Vector3 currentVelocity;
    private float currentDistance;
    private float yaw;
    private float pitch;

    void Start()
    {
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Handle mouse input
        yaw += ControlFreak2.CF2Input.GetAxis("Mouse X") * mouseXSensitivity;
        pitch -= ControlFreak2.CF2Input.GetAxis("Mouse Y") * mouseYSensitivity;
        pitch = Mathf.Clamp(pitch, -40, 85); // Clamp pitch to avoid flipping the camera

        // Calculate the rotation and direction
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.forward;

        // Calculate the desired position with offsets
        Vector3 desiredPosition = target.position - direction * currentDistance + Vector3.up * heightOffset + rotation * Vector3.right * rightOffset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Wall collision handling
        RaycastHit hit;
        if (Physics.Raycast(target.position + Vector3.up * heightOffset, -direction, out hit, currentDistance, cullingLayer))
        {
            if (hit.transform != target)
            {
                currentDistance = Mathf.Clamp(hit.distance - zoomCullingOffset, minDistance, maxDistance);
            }
        }
        else
        {
            currentDistance = maxDistance;
        }

        // Final camera position adjustment with updated distance
        desiredPosition = target.position - direction * currentDistance + Vector3.up * heightOffset + rotation * Vector3.right * rightOffset;
        smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        transform.position = smoothedPosition;

        // Make the camera look at the target's head
        Vector3 lookAtTarget = target.position + Vector3.up * heightOffset + rotation * Vector3.right * rightOffset;
        transform.LookAt(lookAtTarget);
    }
}
