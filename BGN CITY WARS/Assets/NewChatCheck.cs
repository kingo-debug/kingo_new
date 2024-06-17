using UnityEngine;

public class NewChatCheck : MonoBehaviour
    {
    private int lastChildCount;

    void Start()
    {
        lastChildCount = transform.childCount;
    }

    void OnTransformChildrenChanged()
    {
        if (transform.childCount != lastChildCount)
        {
            lastChildCount = transform.childCount;
            OnChildChanged();
        }
    }

    private void OnChildChanged()
    {
        // Call your function here
        Debug.Log("Child count has changed!");
        YourFunction();
    }

    private void YourFunction()
    {
        // Implement your function here
        Debug.Log("YourFunction has been called!");
    }
}


