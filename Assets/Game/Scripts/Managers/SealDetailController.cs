using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SealDetailController : MonoBehaviour
{
    [SerializeField] RectTransform seal_rect; // TEMP
    [SerializeField] Image seal_sprite;

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
    public Image icon_hungerTrait;
    public Image icon_healthTrait;

    public Button feedButton, healButton;

    Seal seal;

    void Start()
    {
        seal = SealManager.Instance.selectedSeal;

        if (seal.hunger <= 30)
        {
            seal_sprite.sprite = SealManager.Instance.seal_stuff.g_small_seal_normal;
            seal_rect.sizeDelta = new Vector2(32, 32);
        }
        else if (seal.hunger > 30 && seal.hunger <= 70)
        {
            seal_sprite.sprite = SealManager.Instance.seal_stuff.g_medium_seal_normal;
            seal_rect.sizeDelta = new Vector2(32, 32);
        }
        else
        {
            seal_sprite.sprite = SealManager.Instance.seal_stuff.g_big_seal_normal;
            seal_rect.sizeDelta = new Vector2(48, 32);
        }

        //healthText.text = $"Health: {seal.health}";
        //moodText.text = $"Mood: {seal.mood}";
        //hungerText.text = $"Hunger: {seal.hunger}";
        healthSlider.SetValue(seal.health);
        //moodSlider.SetValue(seal.mood);
        hungerSlider.SetValue(seal.hunger);

        weightText.text = seal.weight + "KG";



        if (seal.injury == 1)
            injuryText.text = "Net Entanglement";
        else if (seal.injury == 2)
            injuryText.text = "Flipper";
        else if (seal.injury == 3)
            injuryText.text = "Fish Hook";
        else if (seal.injury == 4)
            injuryText.text = "Flu";
        else if (seal.injury == 5)
            injuryText.text = "Orphaned";


        if (!seal.can_feed)
            feedButton.interactable = false;
        else
            feedButton.interactable = true;

        if (!seal.can_heal)
            healButton.interactable = false;
        else
            healButton.interactable = true;

        if (seal.hungerTrait == 1)
        {
            hungerTrait.text = "Fussy";
            icon_hungerTrait.color = Color.red;
        }
        else if (seal.hungerTrait == 2)
        {
            hungerTrait.text = "Normal";
            icon_hungerTrait.color = Color.white;
        }
        else if (seal.hungerTrait == 3)
        {
            hungerTrait.text = "Foodie";
            icon_hungerTrait.color = Color.green;
        }

        if (seal.healthTrait == 1)
        {
            healthTrait.text = "Brittle";
            icon_healthTrait.color = Color.red;
        }
        else if (seal.healthTrait == 2)
        {
            healthTrait.text = "Normal";
            icon_healthTrait.color = Color.white;
        }
        else if (seal.healthTrait == 3)
        {
            healthTrait.text = "Robust";
            icon_healthTrait.color = Color.green;
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
