using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SealDetailController : MonoBehaviour
{
    public Text healthText;
    public Text moodText;
    public Text hungerText;
    public Button backButton;
    public Slider healthSlider;
    public Slider moodSlider;
    public Slider hungerSlider;


    void Start()
    {
        Seal seal = SealManager.Instance.selectedSeal;

        healthText.text = $"Health: {seal.health}";
        moodText.text = $"Mood: {seal.mood}";
        hungerText.text = $"Hunger: {seal.hunger}";
        healthSlider.value = seal.health;
        moodSlider.value = seal.mood;
        hungerSlider.value = seal.hunger;

        backButton.onClick.AddListener(() => SceneManager.LoadScene("HabitatScene"));
    }
}
