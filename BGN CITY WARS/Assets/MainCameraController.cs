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
    public float raycastPadding = 0.1f;      // Extra distance to detect thin surfaces
    public float rayOffsetDistance = 0.5f;   // Distance to offset the side and top rays
    public float cullLerpSpeed = 5.0f;       // Speed of interpolation for smooth culling transition

    public float mouseXSensitivity = 2.0f;   // Mouse X sensitivity
    public float mouseYSensitivity = 2.0f;   // Mouse Y sensitivity

    private Vector3 currentVelocity;
    private float currentDistance;
    private float targetDistance;
    private float yaw;
    private float pitch;

    void Start()
    {
        currentDistance = maxDistance;
        targetDistance = maxDistance;
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

        // Calculate the right offset vector and adjusted target position
        Vector3 rightOffsetVector = rotation * Vector3.right * rightOffset;
        Vector3 targetPositionWithHeight = target.position + Vector3.up * heightOffset;

        // Calculate the desired position with right offset for culling
        Vector3 desiredPositionWithRightOffset = targetPositionWithHeight - direction * maxDistance + rightOffsetVector;

        // Wall collision handling using multiple rays
        Vector3[] raycastOrigins = new Vector3[5];
        raycastOrigins[0] = targetPositionWithHeight + rightOffsetVector;                                    // Center ray
        raycastOrigins[1] = targetPositionWithHeight + rotation * Vector3.right * rayOffsetDistance + rightOffsetVector;  // Right ray
        raycastOrigins[2] = targetPositionWithHeight - rotation * Vector3.right * rayOffsetDistance + rightOffsetVector;  // Left ray
        raycastOrigins[3] = targetPositionWithHeight + rotation * Vector3.up * rayOffsetDistance + rightOffsetVector;    // Up ray
        raycastOrigins[4] = targetPositionWithHeight - rotation * Vector3.up * rayOffsetDistance + rightOffsetVector;    // Down ray

        float closestDistance = maxDistance;
        bool hitSomething = false;
        RaycastHit hit;

        foreach (Vector3 origin in raycastOrigins)
        {
            if (Physics.Raycast(origin, (desiredPositionWithRightOffset - origin).normalized, out hit, maxDistance + raycastPadding, cullingLayer))
            {
                if (hit.transform != target)
                {
                    closestDistance = Mathf.Min(closestDistance, Mathf.Clamp(hit.distance - zoomCullingOffset, minDistance, maxDistance));
                    hitSomething = true;
                }
            }
        }

        if (!hitSomething)
        {
            closestDistance = maxDistance;
        }

        // Smoothly interpolate the distance to prevent bouncing
        targetDistance = Mathf.Lerp(targetDistance, closestDistance, Time.deltaTime * cullLerpSpeed);
        currentDistance = targetDistance;

        // Final camera position adjustment with updated distance and right offset
        Vector3 desiredPosition = targetPositionWithHeight - direction * currentDistance + rightOffsetVector;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        transform.position = smoothedPosition;

        // Make the camera look at the target's head
        Vector3 lookAtTarget = target.position + Vector3.up * heightOffset;
        transform.LookAt(lookAtTarget + rightOffsetVector);

        // Debugging: Draw rays to visualize culling
        foreach (Vector3 origin in raycastOrigins)
        {
            Debug.DrawLine(origin, origin + (desiredPositionWithRightOffset - origin).normalized * (maxDistance + raycastPadding), Color.red);
        }
    }
}
