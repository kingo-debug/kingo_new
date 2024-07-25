using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeToRotate : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform targetToRotate; // Target object to be rotated
    public float rotationSpeed = 0.2f;
    private Vector2 startDragPosition;
    private Vector2 currentDragPosition;
    private Vector2 endDragPosition;
    private bool isDragging = false;

    void Update()
    {
        if (isDragging && targetToRotate != null)
        {
            RotateTarget();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentDragPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDragPosition = eventData.position;
        isDragging = false;
    }

    private void RotateTarget()
    {
        float deltaX = currentDragPosition.x - startDragPosition.x;
        float rotationAmount = deltaX * rotationSpeed;
        targetToRotate.Rotate(Vector3.up, -rotationAmount);
        startDragPosition = currentDragPosition;  // Reset start position to current for continuous rotation
    }
}
