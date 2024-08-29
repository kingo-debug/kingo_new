using UnityEngine;
using UnityEngine.UI;

public class MainMapWindow : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform target; // Assign the object you want to move (e.g., camera target)
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

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isTouching;

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
                        }

                        // Update start position to current for smooth movement
                        startTouchPosition = currentTouchPosition;
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
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
}
