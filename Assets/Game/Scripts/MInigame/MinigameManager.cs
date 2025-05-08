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

    [SerializeField] MinigameGameOverScreen gameover_screen;

    [SerializeField] string text_ending;
    [SerializeField] float text_typespeed_01 = 0.5f, text_typespeed_02 = 2f;

    private void Start()
    {
        current_seal = SealManager.Instance.selectedSeal;
        //SetupMinigame();
        InitGameOverStings();
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
        gameover_screen.gameObject.SetActive(true);
        gameover_screen.text_gameover.text = gameover_stings[Random.Range(0, gameover_stings.Length)];
        yield return new WaitForSecondsRealtime(0.75f);
        gameover_screen.anim.Play("minigameover_start");
        yield return new WaitForSecondsRealtime(gameover_delay);
        gameover_screen.anim.Play("minigameover_end");
        GameManagement.instance.LoadScene("SealDetailScene");
    }

    string[] gameover_stings = new string[6];
    void InitGameOverStings()
    {
        gameover_stings[0] = "GREAT<br>JOB!";
        gameover_stings[1] = "Fantastic!";
        gameover_stings[2] = "Amazing<br>Work!";
        gameover_stings[4] = "Marvelous!";
        gameover_stings[5] = "Sealtastic!";
    }
}
