using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TEMPSealButton : MonoBehaviour
{
    public bool f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!f)
            GetComponent<Button>().onClick.AddListener(() => GiveMeDaSeal());
        else
            GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    public void GiveMeDaSeal()
    {
        SealManager.Instance.SpawnSeal();
        gameObject.SetActive(false);
    }
}
