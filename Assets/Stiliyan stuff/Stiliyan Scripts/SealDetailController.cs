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
    public Text moodTrait;
    public Text hungerTrait;
    public Text healthTrait;


    void Start()
    {
        Seal seal = SealManager.Instance.selectedSeal;

        healthText.text = $"Health: {seal.health}";
        moodText.text = $"Mood: {seal.mood}";
        hungerText.text = $"Hunger: {seal.hunger}";
        healthSlider.value = seal.health;
        moodSlider.value = seal.mood;
        hungerSlider.value = seal.hunger;

        if (seal.moodTrait == 1)
        {
            moodTrait.text = "Lazy";
        }
        else if (seal.moodTrait == 2)
        {
            moodTrait.text = "Normal";
        }
        else if (seal.moodTrait == 3)
        {
            moodTrait.text = "Energetic";
        }

        if (seal.hungerTrait == 1)
        {
            hungerTrait.text = "Fussy";
        }
        else if (seal.hungerTrait == 2)
        {
            hungerTrait.text = "Normal";
        }
        else if (seal.hungerTrait == 3)
        {
            hungerTrait.text = "Foodie";
        }

        if (seal.healthTrait == 1)
        {
            healthTrait.text = "Brittle";
        }
        else if (seal.healthTrait == 2)
        {
            healthTrait.text = "Normal";
        }else if (seal.healthTrait == 3)
        {
            healthTrait.text="Robust";
        }

        backButton.onClick.AddListener(() => SceneManager.LoadScene("HabitatScene"));
    }
}
