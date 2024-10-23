using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;  // The target object you want to track (e.g., the car)
    public Image carIconImage;  // The UI image that will act as the car icon pointing to the target
    public TextMeshProUGUI distanceText; // TextMeshProUGUI element to show distance
    public float edgeOffset = 50f; // Distance from the screen edge when the icon is off-screen

    [Header("Icon Size Settings")]
    public float minSize = 50f; // Minimum size of the icon
    public float maxSize = 150f; // Maximum size of the icon
    public float minDistance = 5f; // Distance at which the icon is at its maximum size
    public float maxDistance = 500f; // Distance at which the icon is at its minimum size

    private Camera mainCamera;
    private RectTransform carIconRectTransform; // Cache RectTransform for performance

    void Start()
    {
        mainCamera = Camera.main;
        carIconRectTransform = carIconImage.rectTransform; // Cache this to avoid repeated GetComponent calls
    }

    void Update()
    {
        if (target == null || !carIconImage.enabled) return; // Exit if no target is set or the icon is disabled

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        // Check if the target is off-screen
        bool isOffScreen = screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

        if (isOffScreen)
        {
            PositionIconAtScreenEdge(screenPos);
        }
        else
        {
            carIconRectTransform.position = screenPos;
        }

        // Update distance text
        UpdateDistanceText();

        // Adjust icon size based on distance
        AdjustIconSize();
    }

    private void PositionIconAtScreenEdge(Vector3 screenPos)
    {
        Vector3 clampedScreenPos = screenPos;
        clampedScreenPos.z = 0;

        if (screenPos.z < 0) // If behind the camera
        {
            screenPos *= -1;
        }

        // Clamp to screen edges with offset
        clampedScreenPos.x = Mathf.Clamp(screenPos.x, edgeOffset, Screen.width - edgeOffset);
        clampedScreenPos.y = Mathf.Clamp(screenPos.y, edgeOffset, Screen.height - edgeOffset);

        carIconRectTransform.position = clampedScreenPos;
        carIconImage.enabled = true; // Ensure the icon is visible
    }

    private void UpdateDistanceText()
    {
        float distance = Vector3.Distance(target.position, mainCamera.transform.position);
        distanceText.text = $"{distance:F1}m";
    }

    private void AdjustIconSize()
    {
        float distance = Vector3.Distance(target.position, mainCamera.transform.position);
        float newSize = CalculateIconSize(distance);
        carIconRectTransform.sizeDelta = new Vector2(newSize, newSize);
    }

    private float CalculateIconSize(float distance)
    {
        // Normalize the distance to a 0-1 range based on minDistance and maxDistance
        float t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        // Lerp between minSize and maxSize based on the normalized distance
        return Mathf.Lerp(maxSize, minSize, t);
    }
}
