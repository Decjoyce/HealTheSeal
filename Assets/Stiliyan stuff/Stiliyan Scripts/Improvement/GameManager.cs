using UnityEngine;
using System;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // An event that notifies listeners when a seal is selected.
    public event Action<SealController> OnSealSelected;
    [SerializeField] private GameObject sealPrefab;
    [SerializeField] private Transform sealParent;  // optional parent transform


    public SealController ActiveSeal { get; private set; }


    public void SpawnNewSeal()
    {
        // Example: pick a random position within your habitat
        Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-3f, 3f));
        GameObject newSeal = Instantiate(sealPrefab, spawnPos, Quaternion.identity, sealParent);

        // Optionally, initialize stats or do something else with the newly spawned seal
        Debug.Log("New seal spawned at: " + spawnPos);
    }

    private void Awake()
    {
        // Set up the GameManager as a singleton.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called by SealController when a seal is clicked.
    /// </summary>
    /// <param name="seal">The SealController of the clicked seal.</param>
    public void SealClicked(SealController seal)
    {
        Debug.Log("GameManager: Seal clicked - " + seal.name);

        // We’ll need to store a reference to this seal for the next scene.
        ActiveSeal = seal;  // We'll add this property in a moment.
        Debug.Log("ActiveSeal is now set to: " + (ActiveSeal != null ? ActiveSeal.name : "null"));

        // Then load the detail scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene("SealDetailScene");
    }
}
