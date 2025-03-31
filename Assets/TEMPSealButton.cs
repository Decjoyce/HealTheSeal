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
            GetComponent<Button>().onClick.AddListener(() => SealManager.Instance.SpawnSeal());
        else
            GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
