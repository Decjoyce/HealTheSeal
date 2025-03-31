using System.Collections;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    Seal current_seal;
    public int minigame_id;

    public int score;
    public int win_score;

    private void Start()
    {
        current_seal = SealManager.Instance.selectedSeal;
        //SetupMinigame();
    }

    void SetupMinigame()
    {
        //win_score =
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if(score >= win_score)
        {
            Win();
        }
    }

    void Win()
    {
        switch (minigame_id)
        {
            case 0:
                current_seal.FeedSeal(10);
                break;
            case 1:
                current_seal.IncreaseHealth(10);
                break;
            case 2:
                current_seal.IncreaseMood(10);
                break;
        }
        GameManagement.instance.LoadScene(2);
    }
}
