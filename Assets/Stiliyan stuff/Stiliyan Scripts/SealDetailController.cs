using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SealDetailController : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moodText;
    public TextMeshProUGUI hungerText;
    public Button backButton;
    public StatsBar healthSlider;
    public StatsBar moodSlider;
    public StatsBar hungerSlider;
    public TextMeshProUGUI moodTrait;
    public TextMeshProUGUI hungerTrait;
    public TextMeshProUGUI healthTrait;


    void Start()
    {
        Seal seal = SealManager.Instance.selectedSeal;

        //healthText.text = $"Health: {seal.health}";
        //moodText.text = $"Mood: {seal.mood}";
        //hungerText.text = $"Hunger: {seal.hunger}";
        healthSlider.SetValue(seal.health);
        moodSlider.SetValue(seal.mood);
        hungerSlider.SetValue(seal.hunger);

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

        backButton.onClick.AddListener(() => GameManagement.instance.LoadScene(1));
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
