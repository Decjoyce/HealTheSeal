using UnityEngine;
using System.Collections.Generic;

public class SealManager : MonoBehaviour
{
    public GameObject sealPrefab; // The seal prefab to spawn
    public Transform spawnArea; // Optional: Define a spawn area
    private List<GameObject> seals = new List<GameObject>(); // Track all seals

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Replace with UI button event later
        {
            SpawnSeal();
        }
    }

    public void SpawnSeal()
    {
        if (sealPrefab == null) return;

        Vector3 randomPosition = GetRandomSpawnPosition();
        GameObject newSeal = Instantiate(sealPrefab, randomPosition, Quaternion.identity);
        seals.Add(newSeal);

        // Assign random stats
        SealStats sealStats = newSeal.GetComponent<SealStats>();
        if (sealStats != null)
        {
            sealStats.InitializeStats(
                health: 50f,
                mood: 50f,
                hunger: 0f
            );
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-7f, 7f); // Adjust based on screen bounds
        float y = Random.Range(-4f, 4f);
        return new Vector3(x, y, 0f);
    }
}
