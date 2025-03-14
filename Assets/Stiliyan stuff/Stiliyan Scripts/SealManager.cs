using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SealManager : MonoBehaviour
{
    public GameObject sealPrefab; // The seal prefab to spawn
    public Transform spawnArea; // Optional: Define a spawn area
    private List<GameObject> seals = new List<GameObject>(); // Track all seals
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////
    /// </summary>
    public Slider healthSlider;
    public Slider moodSlider;
    public Slider hungerSlider;
    public GameObject statsPanel; // The UI panel containing sliders

    public void DisplayStats(SealStats stats)
    {
        if (statsPanel != null)
            statsPanel.SetActive(true); // Show the UI panel

        healthSlider.value = stats.health;
        moodSlider.value = stats.mood;
        hungerSlider.value = stats.hunger;
    }
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

        float healthRange = Random.Range(1f, 25f);
        float moodRange = Random.Range(1f, 50f);
        float hungerRange = Random.Range(1f, 50f);

        // Assign random stats
        SealStats sealStats = newSeal.GetComponent<SealStats>();
        if (sealStats != null)
        {
            sealStats.InitializeStats(
                health: healthRange,
                mood: moodRange,
                hunger: hungerRange
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
