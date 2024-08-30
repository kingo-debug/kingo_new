using UnityEngine;

public class AddOtherEnable : MonoBehaviour
{
    private Enableothers enableothers;
    private DisableOthers disableothers;
    [SerializeField] private string  FindTargetObject;

    void Start()
    {
        // Find the Enableothers component on the "MINI MAP UI" GameObject
        enableothers = GameObject.Find(FindTargetObject).GetComponent<Enableothers>();
        disableothers= GameObject.Find(FindTargetObject).GetComponent<DisableOthers>();

        // Add this object's Transform to the Enableothers.Others array
        AddToOthersArrayEnable(transform);
        // Add this object's Transform to the Enableothers.Others array
        AddToOthersArrayDisable(transform);
    }

    private void AddToOthersArrayEnable(Transform newTransform)
    {
        // Check if the array is null or empty, initialize it if necessary
        if (enableothers.Others == null)
        {
            enableothers.Others = new Transform[0];
        }

        // Step 1: Create a new array with one more slot than the current array
        Transform[] newArray = new Transform[enableothers.Others.Length + 1];

        // Step 2: Copy the existing elements into the new array
        for (int i = 0; i < enableothers.Others.Length; i++)
        {
            newArray[i] = enableothers.Others[i];
        }

        // Step 3: Add the new Transform to the last slot of the new array
        newArray[newArray.Length - 1] = newTransform;

        // Step 4: Replace the old array with the new array
        enableothers.Others = newArray;
    }




    private void AddToOthersArrayDisable(Transform newTransform)
    {
        // Check if the array is null or empty, initialize it if necessary
        if (disableothers.Others == null)
        {
            disableothers.Others = new Transform[0];
        }

        // Step 1: Create a new array with one more slot than the current array
        Transform[] newArray = new Transform[disableothers.Others.Length + 1];

        // Step 2: Copy the existing elements into the new array
        for (int i = 0; i < disableothers.Others.Length; i++)
        {
            newArray[i] = disableothers.Others[i];
        }

        // Step 3: Add the new Transform to the last slot of the new array
        newArray[newArray.Length - 1] = newTransform;

        // Step 4: Replace the old array with the new array
        disableothers.Others = newArray;
    }
}
