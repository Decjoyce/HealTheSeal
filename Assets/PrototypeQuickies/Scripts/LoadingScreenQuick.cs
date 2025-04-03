using System.Collections;
using UnityEngine;

public class LoadingScreenQuick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        yield return new WaitForSecondsRealtime(3f);
        GameManagement.instance.LoadScene(1);
    }
}
