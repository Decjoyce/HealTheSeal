using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    Seal current_seal;
    public int minigame_id;

    public int score;
    public int win_score;

    bool gameover;

    public float gameover_delay = 5f;

    [SerializeField] GameObject gameover_screen;

    private void Start()
    {
        current_seal = SealManager.Instance.selectedSeal;
        //SetupMinigame();
    }

    void SetupMinigame()
    {
        //win_score =
    }

    public void ChangeWinScore(int new_score)
    {
        win_score = new_score;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if(score >= win_score && !gameover)
        {
            Win();
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    void Win()
    {
        gameover = true;
        float delay;
        switch (minigame_id)
        {
            case 0:
                current_seal.FeedSeal(10);

                delay = GameManagement.instance.feed_delay;
                current_seal.can_feed = false;
                GameManagement.instance.AddItemToSchedule_Feed(System.DateTime.Now.AddHours(delay).ToString(), current_seal);
                current_seal.last_time_fed = System.DateTime.Now.ToString();
                current_seal.next_time_feed = System.DateTime.Now.AddHours(delay).ToString();

                AndroidService.instance.QueueNotification_Seal(current_seal, notif_types.feed, System.DateTime.Now.AddHours(delay).ToString());
                break;
            case 1:
                current_seal.IncreaseHealth(10);

                delay = GameManagement.instance.heal_delay;
                current_seal.can_heal = false;
                GameManagement.instance.AddItemToSchedule_Heal(System.DateTime.Now.AddHours(delay).ToString(), current_seal);
                current_seal.last_time_healed = System.DateTime.Now.ToString();
                current_seal.next_time_heal = System.DateTime.Now.AddHours(delay).ToString();

                AndroidService.instance.QueueNotification_Seal(current_seal, notif_types.heal, System.DateTime.Now.AddHours(delay).ToString());
                break;
            case 2:
                //current_seal.IncreaseMood(10);
                break;
        }

        StartCoroutine(EndMinigame());
    }

    IEnumerator EndMinigame()
    {
        gameover_screen.SetActive(true);
        yield return new WaitForSecondsRealtime(0.75f);
        gameover_screen.GetComponent<Animator>().Play("minigameover_start");
        yield return new WaitForSecondsRealtime(gameover_delay);
        GameManagement.instance.LoadScene("SealDetailScene");
    }
}
