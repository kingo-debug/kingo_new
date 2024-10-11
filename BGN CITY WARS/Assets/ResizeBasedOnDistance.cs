using UnityEngine;

public class ResizeBasedOnDistance : MonoBehaviour
{
    public Transform target1; // First target
    public Transform target2; // Second target

    public float minSize = 0.5f; // Minimum scale limit
    public float maxSize = 2.0f; // Maximum scale limit

    public float distanceFactor = 0.1f; // Factor to scale the size by distance

    private Vector3 initialScale; // Initial scale of the object

    void Start()
    {
        // Store the initial scale of the object
        initialScale = transform.localScale;

    }

    void Update()
    {
        target2 = Camera.main.transform;
        if (target1 != null && target2 != null)
        {

            // Calculate the distance between the two targets
            float distance = Vector3.Distance(target1.position, target2.position);

            // Calculate the new scale based on the distance and factor
            float newScale = distance * distanceFactor;

            // Clamp the scale between minSize and maxSize
            newScale = Mathf.Clamp(newScale, minSize, maxSize);

            // Apply the new scale to the object's initial scale
            transform.localScale = initialScale * newScale;
        }
    }
}
