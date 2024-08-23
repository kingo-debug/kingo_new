using UnityEngine;

public class SmoothFollower : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Target to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target

    [Header("Smoothness Settings")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; // Control smoothness of the follow movement (lower value = smoother)

    [Header("Rotation Settings")]
    public bool inheritXRotation = false; // Inherit X rotation
    public bool inheritYRotation = false; // Inherit Y rotation
    public bool inheritZRotation = false; // Inherit Z rotation

    private void LateUpdate()
    {
        if (target == null) return;

        // Desired position with the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

       
            Vector3 newRotation = transform.eulerAngles;

            if (inheritXRotation) newRotation.x = target.eulerAngles.x;
            if (inheritYRotation) newRotation.y = target.eulerAngles.y;
            if (inheritZRotation) newRotation.z = target.eulerAngles.z;

            transform.rotation = Quaternion.Euler(newRotation);
        }
    
}
