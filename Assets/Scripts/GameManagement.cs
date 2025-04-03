using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    public static GameManagement instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of GAMEMANAGEMENT found");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int index = -1, string scene_name = "")
    {
        if (index != -1)
            SceneManager.LoadScene(index);
        else if(scene_name != "")
            SceneManager.LoadScene(index);
        else
        {
            Debug.LogWarning("Scene Name: " + scene_name + " | Scene Index: " + index + " does not exist");
            return;
        }
    }

}
