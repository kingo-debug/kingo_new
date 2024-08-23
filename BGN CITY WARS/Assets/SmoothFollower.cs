using UnityEngine;

public class SmoothFollower : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Target to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target

    [Header("Smoothness Settings")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; // Control smoothness of the follow movement (lower value = smoother)

    private void LateUpdate()
    {
        if (target == null) return;

        // Desired position with the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
