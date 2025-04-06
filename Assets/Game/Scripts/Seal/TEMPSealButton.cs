using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TEMPSealButton : MonoBehaviour
{
    //Using 2 bools to differentiate between button and seal
    public bool isBackButton = false;
    public bool needsRescue = true; // NEW bool to indicate rescuable seals clearly.

    void Start()
    {
        if (!isBackButton)
            GetComponent<Button>().onClick.AddListener(GiveMeDaSeal);
        else
            GetComponent<Button>().onClick.AddListener(GoBack);
    }

    void GiveMeDaSeal()
    {
        if (needsRescue)
        {
            Seal newSeal = new Seal();
            newSeal.RandomizeAttributes();
            SealManager.Instance.seals.Add(newSeal);
            SealManager.Instance.selectedSeal = newSeal;
            SealManager.Instance.justRescuedSeal = true;
        }

        // Hide seal visually
        gameObject.SetActive(false);
    }

    void GoBack()
    {
        if (SealManager.Instance.justRescuedSeal)
        {
            SealManager.Instance.justRescuedSeal = false;
            SceneManager.LoadScene("Minigame_Heal"); // NEW Scene after rescue
        }
        else
        {
            SceneManager.LoadScene("HabitatScene");
        }
    }
}
