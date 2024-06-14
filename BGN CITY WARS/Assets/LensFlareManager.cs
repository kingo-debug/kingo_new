using UnityEngine;
using UnityEngine.UI;

public class LensFlareManager : MonoBehaviour
{
    public Transform target; // The target the lens flares will follow
    public float movementSpeed = 5f; // Speed of the lens flares movement

    private RectTransform[] lensFlares;

    void Start()
    {
        // Get all the UI Image components under this GameObject
        lensFlares = GetComponentsInChildren<RectTransform>();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(target.position);

            foreach (RectTransform flare in lensFlares)
            {
                // Move the lens flare towards the target position
                flare.position = Vector3.Lerp(flare.position, targetPosition, movementSpeed * Time.deltaTime);
            }
        }
    }
}
