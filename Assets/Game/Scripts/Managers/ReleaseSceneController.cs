using UnityEngine;
using UnityEngine.SceneManagement;

public class ReleaseSceneController : MonoBehaviour
{
    private Seal sealToRelease;

    void Start()
    {
        sealToRelease = SealManager.Instance.selectedSeal;
    }

    // Call this method when your release action is finished (e.g., after animation or button click)
    public void CompleteRelease()
    {
        if (sealToRelease != null)
        {
            // Remove seal from the SealManager
            SealManager.Instance.seals.RemoveAll(s => s.id == sealToRelease.id);
            SealManager.Instance.selectedSeal = null;
        }

        // Go back to the Habitat scene
        SceneManager.LoadScene("HabitatScene");
    }
}
