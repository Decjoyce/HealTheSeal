using UnityEngine;
using UnityEngine.SceneManagement;

public class PostRescueMinigameController : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("SealDetailScene");
    }
    // Call this method when the minigame is complete:
    public void OnMinigameCompleted()
    {
        // Optionally increase stats automatically for rescue
        Seal seal = SealManager.Instance.selectedSeal;
        //seal.mood += 10; // or health/hunger based on your desired logic

        // Load seal detailed view after minigame
        SceneManager.LoadScene("SealDetailScene");

    }
}
