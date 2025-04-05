using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostRescueMinigameController : MonoBehaviour
{

    public void Update()
    {
        GetComponent<Button>().onClick.AddListener(OnMinigameCompleted);
    }
    // Call this method when the minigame is complete:
    public void OnMinigameCompleted()
    {
        // Optionally increase stats automatically for rescue
        Seal seal = SealManager.Instance.selectedSeal;
        seal.mood += 10; // or health/hunger based on your desired logic

        // Load seal detailed view after minigame
        SceneManager.LoadScene("SealDetailScene");



       
    }
}
