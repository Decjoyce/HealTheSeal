using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SealDetailController : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI moodText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI injuryText;

    public Button backButton;
    public StatsBar healthSlider; //Prototype - Changed from slider to custom script
    public StatsBar moodSlider; //Prototype - Changed from slider to custom script
    public StatsBar hungerSlider;//Prototype - Changed from slider to custom script
    public TextMeshProUGUI moodTrait;
    public TextMeshProUGUI hungerTrait;
    public TextMeshProUGUI healthTrait;

    public Button feedButton, healButton;

    Seal seal;

    void Start()
    {
        seal = SealManager.Instance.selectedSeal;

        //healthText.text = $"Health: {seal.health}";
        //moodText.text = $"Mood: {seal.mood}";
        //hungerText.text = $"Hunger: {seal.hunger}";
        healthSlider.SetValue(seal.health);
        //moodSlider.SetValue(seal.mood);
        hungerSlider.SetValue(seal.hunger);

        if (!seal.can_feed)
            feedButton.interactable = false;
        else
            feedButton.interactable = true;

        if (!seal.can_heal)
            healButton.interactable = false;
        else
            healButton.interactable = true;

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

        backButton.onClick.AddListener(() => GameManagement.instance.LoadScene("HabitatScene")); //Prototype - 
    }

    private void Update()
    {
        if (!seal.can_feed)
            feedButton.interactable = false;
        else
            feedButton.interactable = true;

        if (!seal.can_heal)
            healButton.interactable = false;
        else
            healButton.interactable = true;
    }

    public void LoadMinigameScene(int mg_type) // 0 - Hunger, 1 - Health
    {
        int ranNum = Random.Range(0, 3);
        if(mg_type == 0)
        {
            switch (ranNum)
            {
                case 0:
                    GameManagement.instance.LoadScene("MG_F_FlickFish");
                    break;
                case 1:
                    GameManagement.instance.LoadScene("MG_F_Iceblock");
                    break;
                case 2:
                    GameManagement.instance.LoadScene("MG_F_Snuffle");
                    break;
            }
        }
        else if(mg_type == 1)
        {
            switch (ranNum)
            {
                case 0:
                    GameManagement.instance.LoadScene("MG_H_Medicine");
                    break;
                case 1:
                    GameManagement.instance.LoadScene("MG_H_Spray");
                    break;
                case 2:
                    GameManagement.instance.LoadScene("MG_H_Medicine"); // Change to new game
                    break;
            }
        }
    }
}
