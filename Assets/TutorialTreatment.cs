using UnityEngine;

public class TutorialTreatment : MonoBehaviour
{
    [SerializeField] GameObject[] sections;

    [SerializeField] GameObject bg;

    private void Start()
    {
        if (!GameManagement.instance.first_time)
            Destroy(gameObject);
    }

    public void IncreaseTutIndex(int i = 1)
    {
        GameManagement.instance.tutorial_index += i;
    }

    public void SprayedSeal()
    {
        GameManagement.instance.tutorial_index++;
        sections[3].SetActive(false);
        sections[4].SetActive(true);
    }

    public void BandagedSeal()
    {
        GameManagement.instance.tutorial_index++;
        sections[4].SetActive(false);
        sections[5].SetActive(true);
    }

    public void CagedSeal()
    {
        GameManagement.instance.tutorial_index++;
        sections[5].SetActive(false);
        sections[6].SetActive(true);
        bg.SetActive(true);
    }
}
