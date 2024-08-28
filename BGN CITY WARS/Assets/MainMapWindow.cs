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

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isTouching;

    private void Update()
    {
        HandleTouchInput();
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
                                float moveAmountX = swipeDelta.x * moveSpeed * Time.deltaTime;
                                MoveHorizontal(moveAmountX);
                            }
                            else
                            {
                                // Vertical swipe (up/down)
                                float moveAmountZ = swipeDelta.y * moveSpeed * Time.deltaTime;
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
        Vector3 newPosition = target.position;
        newPosition.x += moveAmountX;

        // Clamp the X position to the defined limits
        newPosition.x = Mathf.Clamp(newPosition.x, xClamp.x, xClamp.y);

        target.position = newPosition;
    }

    private void MoveVertical(float moveAmountZ)
    {
        Vector3 newPosition = target.position;
        newPosition.z += moveAmountZ;

        // Clamp the Z position to the defined limits
        newPosition.z = Mathf.Clamp(newPosition.z, zClamp.x, zClamp.y);

        target.position = newPosition;
    }
}
