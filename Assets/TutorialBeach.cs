using UnityEngine;
using UnityEngine.UI;

public class TutorialBeach : MonoBehaviour
{

    [SerializeField] GameObject[] sections;

    [SerializeField] Button back_button, seal_button;

    private void Start()
    {
        if (!GameManagement.instance.first_time)
            Destroy(gameObject);
        else
            back_button.gameObject.SetActive(false);
    }

    public void IncreaseTutIndex(int i = 1)
    {
        GameManagement.instance.tutorial_index += i;
    }

    public void RescuedSeal()
    {
        GameManagement.instance.tutorial_index++;
        sections[4].SetActive(false);
        sections[5].SetActive(true);
        back_button.gameObject.SetActive(true);
    }
    
    public void ReturnedHome()
    {
        GameManagement.instance.tutorial_index++;
    }

}
