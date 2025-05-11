using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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

    [SerializeField] Sprite[] timeout_heart, timeout_feed;

    float total_time_dif_heal, total_time_dif_feed;

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

        if(!seal.can_feed)
            total_time_dif_feed = (float) (System.DateTime.Parse(seal.next_time_feed) - System.DateTime.Parse(seal.last_time_fed)).TotalDays;

        if (!seal.can_heal)
            total_time_dif_heal = (float)(System.DateTime.Parse(seal.next_time_heal) - System.DateTime.Parse(seal.last_time_healed)).TotalDays;

        if (!seal.can_feed)
            StartCoroutine(CheckingHunger());
        if (!seal.can_heal)
            StartCoroutine(CheckingHealth());
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

    IEnumerator CheckingHunger()
    {
        while (!seal.can_feed)
        {
            float time_diff_feed = (float)(System.DateTime.Parse(seal.next_time_feed) - System.DateTime.Now).TotalDays;
            float time_dif_percent = (total_time_dif_feed - time_diff_feed) / total_time_dif_feed;

            if (time_dif_percent > 0 && time_dif_percent <= 0.1)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[0];
            }
            else if (time_dif_percent > 0.1 && time_dif_percent <= 0.2)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[1];
            }
            else if (time_dif_percent > 0.2 && time_dif_percent <= 0.3)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[2];
            }
            else if (time_dif_percent > 0.3 && time_dif_percent <= 0.4)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[3];
            }
            else if (time_dif_percent > 0.4 && time_dif_percent <= 0.5)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[4];
            }
            else if (time_dif_percent > 0.5 && time_dif_percent <= 0.6)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[5];
            }
            else if (time_dif_percent > 0.6 && time_dif_percent <= 0.7)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[6];
            }
            else if (time_dif_percent > 0.7 && time_dif_percent <= 0.8)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[7];
            }
            else if (time_dif_percent > 0.8 && time_dif_percent <= 0.9)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[8];
            }
            else if (time_dif_percent > 0.9 && time_dif_percent <= 1)
            {
                feedButton.GetComponent<Image>().sprite = timeout_feed[9];
            }

            yield return new WaitForSeconds(1);
        }
        feedButton.GetComponent<Image>().sprite = timeout_feed[10];
    }

    IEnumerator CheckingHealth()
    {
        while (!seal.can_heal)
        {
            float time_diff_heal = (float) (System.DateTime.Parse(seal.next_time_heal) - System.DateTime.Now).TotalDays;
            float time_dif_percent = (total_time_dif_heal - time_diff_heal) / total_time_dif_heal;

            if (time_dif_percent > 0 && time_dif_percent <= 0.1)
            {
                Debug.Log("he");
                healButton.GetComponent<Image>().sprite = timeout_heart[0];
            }
            else if (time_dif_percent > 0.1 && time_dif_percent <= 0.2)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[1];
            }
            else if (time_dif_percent > 0.2 && time_dif_percent <= 0.3)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[2];
            }
            else if (time_dif_percent > 0.3 && time_dif_percent <= 0.4)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[3];
            }
            else if (time_dif_percent > 0.4 && time_dif_percent <= 0.5)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[4];
            }
            else if (time_dif_percent > 0.5 && time_dif_percent <= 0.6)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[5];
            }
            else if (time_dif_percent > 0.6 && time_dif_percent <= 0.7)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[6];
            }
            else if (time_dif_percent > 0.7 && time_dif_percent <= 0.8)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[7];
            }
            else if (time_dif_percent > 0.8 && time_dif_percent <= 0.9)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[8];
            }
            else if (time_dif_percent > 0.9 && time_dif_percent <= 1)
            {
                healButton.GetComponent<Image>().sprite = timeout_heart[9];
            }

            yield return new WaitForSeconds(1);
        }
        healButton.GetComponent<Image>().sprite = timeout_heart[10];
    }

    public void LoadMinigameScene(int mg_type) // 0 - Hunger, 1 - Health
    {
        int ranNum = Random.Range(0, 3);
        if(mg_type == 0)
        {
            switch (ranNum)
            {
                case 0:
                    GameManagement.instance.LoadScene("MG_F_FlickFish", transition_index: 4);
                    break;
                case 1:
                    GameManagement.instance.LoadScene("MG_F_Iceblock", transition_index: 4);
                    break;
                case 2:
                    GameManagement.instance.LoadScene("MG_F_Snuffle", transition_index: 4);
                    break;
            }
        }
        else if(mg_type == 1)
        {
            switch (ranNum)
            {
                case 0:
                    GameManagement.instance.LoadScene("MG_H_Medicine", transition_index: 3);
                    break;
                case 1:
                    GameManagement.instance.LoadScene("MG_H_Spray", transition_index: 3);
                    break;
                case 2:
                    GameManagement.instance.LoadScene("MG_H_Medicine", transition_index: 3); // Change to new game
                    break;
            }
        }
    }
}
