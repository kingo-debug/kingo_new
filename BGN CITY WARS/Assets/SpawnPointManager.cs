using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is a spawn zone (you can tag your zones or use other identifiers)
        if (other.CompareTag("SP"))
        {
            // Get all child transforms of the zone
            Transform[] zoneSpawnPoints = other.GetComponentsInChildren<Transform>();

            foreach (Transform point in zoneSpawnPoints)
            {
                // Add the child transform as a spawn point if it's not already in the list
                if (!spawnPoints.Contains(point) && point != other.transform)
                {
                    spawnPoints.Add(point);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object is a spawn zone
        if (other.CompareTag("SP"))
        {
            // Clear the spawn points list when leaving the zone
            spawnPoints.Clear();
        }
    }
}
