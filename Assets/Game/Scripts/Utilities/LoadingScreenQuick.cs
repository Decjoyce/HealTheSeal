using System.Collections;
using UnityEngine;

public class LoadingScreenQuick : MonoBehaviour
{
    [SerializeField] float delay;
    public string scene_to_switch_to;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        yield return new WaitForSecondsRealtime(delay);
        GameManagement.instance.LoadScene(scene_to_switch_to);
    }
}
