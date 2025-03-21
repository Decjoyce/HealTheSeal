using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SealDetailController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider moodSlider;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;

    private SealController currentSeal;

    private void Start()
    {
        Debug.Log("Detail scene sees ActiveSeal: " + (GameManager.Instance.ActiveSeal != null));

        // Get the active seal from the GameManager
        currentSeal = GameManager.Instance.ActiveSeal;

        if (currentSeal == null)
        {
            Debug.LogError("No seal data found. Returning to Habitat Scene.");
            SceneManager.LoadScene("HabitatScene");
            return;
        }

        // Initialize the sliders
        moodSlider.value = currentSeal.sealData.mood;
        healthSlider.value = currentSeal.sealData.health;
        hungerSlider.value = currentSeal.sealData.hunger;
    }

    public void OnBackButtonClicked()
    {
        // Return to the habitat
        SceneManager.LoadScene("HabitatScene");
    }
    public void OnFeedSealButtonClicked()
    {
        // Increase hunger
        currentSeal.sealData.hunger = Mathf.Min(currentSeal.sealData.hunger + 10f, 100f);
        // Refresh UI
        hungerSlider.value = currentSeal.sealData.hunger;
    }

}
