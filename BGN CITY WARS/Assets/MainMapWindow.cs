using UnityEngine;
using UnityEngine.UI;

public class MainMapWindow : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform target; // Assign the object you want to move (e.g., camera target)
    public Transform playerTarget; // Assign the PlayerTarget you want to recenter to
    public float moveSpeed = 10f; // Speed at which the target moves
    public Vector2 xClamp = new Vector2(-50f, 50f); // Clamp for X axis movement (left/right)
    public Vector2 zClamp = new Vector2(-50f, 50f); // Clamp for Z axis movement (up/down)

    [Header("UI Boundary")]
    [SerializeField] private RectTransform swipeArea; // Assign the UI boundary (e.g., an invisible UI image)

    [Header("Zoom Settings")]
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private Slider zoomSlider; // Reference to the slider for zoom control
    public float minZoom = 5f; // Minimum orthographic size
    public float maxZoom = 20f; // Maximum orthographic size

    [Header("Point Marker Settings")]
    [SerializeField] private GameObject pointMarkerPrefab; // Prefab for the Point Marker
    private GameObject currentPointMarker; // Reference to the currently active Point Marker

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isTouching;
    private bool isSwipe;

    private Vector3 targetPosition;

    private void Start()
    {
        // Initialize the target position with the current position
        targetPosition = target.position;

        // Initialize the slider values based on the camera's orthographic size
        if (zoomSlider != null && mainCamera != null && mainCamera.orthographic)
        {
            zoomSlider.minValue = minZoom;
            zoomSlider.maxValue = maxZoom;
            zoomSlider.value = mainCamera.orthographicSize;
        }
    }

    private void Update()
    {
        HandleTouchInput();

        // Smoothly interpolate the target's position
        target.position = Vector3.Lerp(target.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is within the swipe area
            if (RectTransformUtility.RectangleContainsScreenPoint(swipeArea, touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isTouching = true;
                        isSwipe = false;
                        startTouchPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        currentTouchPosition = touch.position;

                        // Calculate swipe direction
                        Vector2 swipeDelta = currentTouchPosition - startTouchPosition;

                        if (isTouching)
                        {
                            // Determine if swipe is more horizontal or vertical
                            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                            {
                                // Horizontal swipe (left/right)
                                float moveAmountX = swipeDelta.x * moveSpeed * 0.01f; // Adjusted for smoother movement
                                MoveHorizontal(moveAmountX);
                            }
                            else
                            {
                                // Vertical swipe (up/down)
                                float moveAmountZ = swipeDelta.y * moveSpeed * 0.01f; // Adjusted for smoother movement
                                MoveVertical(moveAmountZ);
                            }
                            isSwipe = true; // If there is movement, it is considered a swipe
                        }

                        // Update start position to current for smooth movement
                        startTouchPosition = currentTouchPosition;
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (!isSwipe) // Only interact with point marker if it wasn't a swipe
                        {
                            HandlePointMarker();
                        }
                        isTouching = false;
                        break;
                }
            }
        }
    }

    private void MoveHorizontal(float moveAmountX)
    {
        targetPosition.x += moveAmountX;

        // Clamp the X position to the defined limits
        targetPosition.x = Mathf.Clamp(targetPosition.x, xClamp.x, xClamp.y);
    }

    private void MoveVertical(float moveAmountZ)
    {
        targetPosition.z += moveAmountZ;

        // Clamp the Z position to the defined limits
        targetPosition.z = Mathf.Clamp(targetPosition.z, zClamp.x, zClamp.y);
    }

    // Function to be called by the Slider to control camera zoom
    public void OnZoomSliderChanged()
    {
        if (mainCamera != null && mainCamera.orthographic)
        {
            mainCamera.orthographicSize = Mathf.Clamp(zoomSlider.value, minZoom, maxZoom);
        }
    }

    // Function to recenter the target's position to match PlayerTarget's X and Z positions
    public void RecenterToPlayerTarget()
    {
        Vector3 playerTargetPosition = playerTarget.position;

        // Update only the X and Z position of the target, ignore Y
        targetPosition.x = playerTargetPosition.x;
        targetPosition.z = playerTargetPosition.z;

        // Apply clamping to ensure the position is within bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, xClamp.x, xClamp.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, zClamp.x, zClamp.y);
    }

    // Function to handle the spawning or removal of a Point Marker
    private void HandlePointMarker()
    {
        if (currentPointMarker != null)
        {
            // Destroy the existing Point Marker if one exists
            Destroy(currentPointMarker);
            currentPointMarker = null; // Clear the reference
        }
        else
        {
            // If no Point Marker exists, spawn a new one
            SpawnPointMarker();
        }
    }

    // Function to spawn a Point Marker at the target's X and Z position, with Y set to 0
    private void SpawnPointMarker()
    {
        if (pointMarkerPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(target.position.x, 0, target.position.z);
            currentPointMarker = Instantiate(pointMarkerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Point Marker Prefab is not assigned!");
        }
    }
}
