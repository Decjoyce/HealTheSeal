using UnityEngine;
using UnityEngine.UI;

public class HabitatUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button spawnSealButton;   // The button that spawns a new seal
    [SerializeField] private GameObject highlightIcon; // Optional: exclamation icon to show when a new seal is available

    // Example: If you want to simulate new seals becoming available automatically
    // you could have a timer or a method that triggers after some condition is met.
    [Header("Spawn Timer (Optional)")]
    [SerializeField] private float timeBetweenSpawns = 10f;
    private float spawnTimer;
    private bool newSealAvailable;


    private void Start()
    {
        // Hook up the button click event to our SpawnSeal method.
        spawnSealButton.onClick.AddListener(SpawnSeal);

        // Initially hide the highlight icon (exclamation mark).
        if (highlightIcon != null)
            highlightIcon.SetActive(false);

        // Initialize the timer or any state needed for spawn availability.
        spawnTimer = timeBetweenSpawns;
        newSealAvailable = false;
        
    }

    private void Update()
    {
        // OPTIONAL: If you want a new seal to become available after a certain time,
        // we decrement the timer here.
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f && !newSealAvailable)
        {
            // A new seal is "found" and can be spawned.
            newSealAvailable = true;

            // Show the exclamation icon or highlight.
            if (highlightIcon != null)
                highlightIcon.SetActive(true);
        }
    }

    /// <summary>
    /// Called when the Spawn Seal button is clicked.
    /// </summary>
    private void SpawnSeal()
    {
        // Only spawn a seal if one is actually available.
        if (newSealAvailable)
        {
            // Call the GameManager to spawn a new seal in the habitat.
            GameManager.Instance.SpawnNewSeal();

            // Reset availability so the player can’t keep spawning.
            newSealAvailable = false;
            spawnTimer = timeBetweenSpawns;

            // Hide the highlight icon again.
            if (highlightIcon != null)
                highlightIcon.SetActive(false);
        }
        else
        {
            // If no new seal is available yet, you could show a message or do nothing.
            Debug.Log("No new seal available yet!");
        }
    }
}
