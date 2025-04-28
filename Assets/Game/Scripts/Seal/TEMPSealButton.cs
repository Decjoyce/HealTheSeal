using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TEMPSealButton : MonoBehaviour
{
    //Using 2 bools to differentiate between button and seal
    public bool isBackButton = false;
    //public bool needsRescue = true; //indicate rescuable seals clearly

    public Sprite netSprite;
    public Sprite hookSprite;
    public Sprite antibioticsSprite;
    public Sprite healthySprite;

    void Start()
    {
        if (!isBackButton)
        {
            bool available = SealManager.Instance.isSealAvailableForRescue;
            gameObject.SetActive(available);

            if (available)
            {
                SetCorrectSprite();
                GetComponent<Button>().onClick.AddListener(GiveMeDaSeal);
            }
        }
        else
        {
            GetComponent<Button>().onClick.AddListener(GoBack);
        }
    }
    void SetCorrectSprite()
    {
        Image img = GetComponent<Image>();

        img.sprite = SealManager.Instance.GetSealInjuryGraphics();
    }

    void GiveMeDaSeal()
    {
        if (SealManager.Instance.currentSealNeedsRescue)
        {
            // Seal actually needs rescue
            SealManager.Instance.isSealAvailableForRescue = false;

            Seal newSeal = new Seal();
            newSeal.RandomizeAttributes();
            newSeal.injury = SealManager.Instance.currentSealInjury; // copy injury

            SealManager.Instance.seals.Add(newSeal);
            SealManager.Instance.selectedSeal = newSeal;
            newSeal.colour_scheme = SealManager.Instance.seal_stuff.GetRandomColourScheme();
            SealManager.Instance.justRescuedSeal = true;

            gameObject.SetActive(false); // hide seal visually
        }
        else
        {
            // Seal is healthy; animate off-screen or hide clearly
            StartCoroutine(MoveSealOffScreen());
            SealManager.Instance.isSealAvailableForRescue = false; // no longer available
        }
    }

    IEnumerator MoveSealOffScreen()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 targetPos = rt.anchoredPosition + new Vector2(1000, 0); // move off-screen right
        float elapsed = 0, duration = 1f;

        while (elapsed < duration)
        {
            rt.anchoredPosition = Vector3.Lerp(rt.anchoredPosition, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false); // finally hide button
    }

    void GoBack()
    {
        if (SealManager.Instance.justRescuedSeal)
        {
            SealManager.Instance.justRescuedSeal = false;

            // Load minigame based on injury type

            //switch (SealManager.Instance.selectedSeal.injury)
            //{
            //    case 1: SceneManager.LoadScene("NetRemovalMinigame"); break;
            //    case 2: SceneManager.LoadScene("HookRemovalMinigame"); break;
            //    case 3: SceneManager.LoadScene("DisinfectionMinigame"); break;
            //    default: SceneManager.LoadScene("HabitatScene"); break;
            //}
            GameManagement.instance.LoadScene("MG_Treatment");
        }
        else
        {
            GameManagement.instance.LoadHabitatScene();
        }
    }
}
