using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TutorialHabitat : MonoBehaviour
{

    [SerializeField] GameObject bg, section_6, section_7, section_8;

    [SerializeField] GameObject[] sections;

    [SerializeField] Button beach_button, icu_button, kennels_button;

    [SerializeField] CameraScroll scrol;

    private void Start()
    {
        if (!GameManagement.instance.first_time)
            Destroy(gameObject);

        if(GameManagement.instance.tutorial_index > 20)
        {
            sections[0].SetActive(false);
            sections[8].SetActive(true);
            scrol.can_scroll = false;
        }
    }

    public void IncreaseTutIndex(int i = 1)
    {
        GameManagement.instance.tutorial_index += i;
    }

    public void YayTutorial()
    {
        GameManagement.instance.BeginTutorial();
    }

    public void NayTutorial()
    {
        GameManagement.instance.tutorial = false;
        scrol.can_scroll = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (GameManagement.instance.tutorial_index == 6 && Input.GetMouseButtonDown(0))
        {
            Start_TutorialSection0708();
            section_6.SetActive(false);
            GameManagement.instance.tutorial_index++;
        }
    }

    public void Start_TutorialSection0708()
    {
        StartCoroutine(TutorialSection0708());
    }

    IEnumerator TutorialSection0708()
    {
        SealManager.Instance.ScheduleSealToBeRescuedTime();
        yield return new WaitForSecondsRealtime(2f);
        section_7.SetActive(true);
        GameManagement.instance.tutorial_index++;
        yield return new WaitForSecondsRealtime(2f);
        GameManagement.instance.tutorial_index++;
        section_7.SetActive(false);
        section_8.SetActive(true);
        beach_button.interactable = true;
    }

    public void ClickedOnICU()
    {
        sections[10].SetActive(false);
        sections[11].SetActive(true);
    }

}
