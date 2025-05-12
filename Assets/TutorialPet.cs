using UnityEngine;

public class TutorialPet : MonoBehaviour
{
    private void Start()
    {
        if (!GameManagement.instance.first_time)
            Destroy(gameObject);
    }

    public void IncreaseTutIndex(int i = 1)
    {
        GameManagement.instance.tutorial_index += i;
    }

    public void EndItAll()
    {
        Destroy(gameObject);
        GameManagement.instance.tutorial = false;
        GameData.instance.SaveAllData();
        GameManagement.instance.first_time = false;
        GameData.instance.gd_sealdata.done_tutorial = true;
    }
}
