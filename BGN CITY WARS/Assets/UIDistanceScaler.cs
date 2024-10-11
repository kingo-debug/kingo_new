using UnityEngine;

public class ObjectDistanceScaler : MonoBehaviour
{
    public Camera mainCamera; // The main camera in the scene

    public float minScale = 0.5f; // Minimum scale size when camera is far away
    public float maxScale = 2f; // Maximum scale size when camera is close
    public float minDistance = 5f; // Distance at which scaling starts increasing
    public float maxDistance = 50f; // Distance at which scaling stops

    private Transform target; // The target GameObject

    private void Start()
    {
        // Set the target to the GameObject this script is attached to
        target = transform;
    }

    private void Update()
    {
        // Calculate the distance between the camera and the target
        float distance = Vector3.Distance(mainCamera.transform.position, target.position);

        // Clamp the distance between minDistance and maxDistance
        float clampedDistance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculate the scale factor based on the distance, inversely proportional
        float scale = Mathf.Lerp(maxScale, minScale, (clampedDistance + minDistance) / (maxDistance + minDistance));

        // Apply the scale to the target GameObject
        target.localScale = new Vector3(scale, scale, scale);


    }
}
