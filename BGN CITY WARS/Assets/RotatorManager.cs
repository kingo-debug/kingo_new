using UnityEngine;
public class RotatorManager : MonoBehaviour
{
    public Transform[] lootObjects;
    public float rotationSpeed = 30f;

    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        foreach (Transform loot in lootObjects)
        {
            loot.Rotate(Vector3.up * rotationAmount);
        }
    }
}
