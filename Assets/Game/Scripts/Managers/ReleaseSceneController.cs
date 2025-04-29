using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReleaseSceneController : MonoBehaviour
{
    private Seal sealToRelease;


    void Start()
    {
        sealToRelease = SealManager.Instance.selectedSeal;
        StartCoroutine(CompleteRelease());
    }

    // Call this method when your release action is finished (e.g., after animation or button click)
    public IEnumerator CompleteRelease()
    {
        SealManager.Instance.seals.RemoveAll(s => s.id == sealToRelease.id);
        SealManager.Instance.selectedSeal = null;
        yield return new WaitForSecondsRealtime(8f);
        GameManagement.instance.LoadHabitatScene();
    }
}
