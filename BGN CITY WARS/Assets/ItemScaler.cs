using UnityEngine;

public class ItemScaler : MonoBehaviour
{
    public Camera mapCamera;
    public float minScale = 0.5f; // Minimum scale of the item when fully zoomed in
    public float maxScale = 2.0f; // Maximum scale of the item when fully zoomed out
    public float minCameraSize = 5.0f; // Define the minimum orthographic size of the camera (zoomed in)
    public float maxCameraSize = 20.0f; // Define the maximum orthographic size of the camera (zoomed out)

    private Vector3 initialScale;
    private float previousCameraSize;

    private void Start()
    {
        mapCamera = GameObject.Find("MAP CAMERAS").transform.GetChild(0).GetComponent<Camera>();
        if (mapCamera == null)
        {
            mapCamera = Camera.main;
        }

        initialScale = transform.localScale;
        previousCameraSize = mapCamera.orthographicSize;
        AdjustScale(); // Set the initial scale based on the starting camera size
    }

    private void LateUpdate()
    {
        // Only adjust scale if the camera's orthographic size has changed
        if (mapCamera.orthographicSize != previousCameraSize)
        {
            AdjustScale();
            previousCameraSize = mapCamera.orthographicSize;
        }
    }

    private void AdjustScale()
    {
        // Clamp the camera size to ensure it is within defined limits
        float clampedCameraSize = Mathf.Clamp(mapCamera.orthographicSize, minCameraSize, maxCameraSize);

        // Calculate the scale factor based on the camera size
        float t = (clampedCameraSize - minCameraSize) / (maxCameraSize - minCameraSize);
        float scaleFactor = Mathf.Lerp(maxScale, minScale, t);

        // Apply the scale to the item
        transform.localScale = initialScale * scaleFactor;
    }

}
