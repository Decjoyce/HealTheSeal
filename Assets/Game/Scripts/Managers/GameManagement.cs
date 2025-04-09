using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    public static GameManagement instance;

    public bool dev_mode; //{ get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one instance of GAMEMANAGEMENT found");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string scene_name = "", int index = -1)
    {
        if (scene_name != "")
            SceneManager.LoadScene(scene_name);
        else if(index != -1)
            SceneManager.LoadScene(index);
        else
        {
            Debug.LogWarning("Scene Name: " + scene_name + " | Scene Index: " + index + " does not exist");
            return;
        }
        GameData.instance.SaveAllData();
    }


    public void EnterDebugMode()
    {
        dev_mode = true;
    }
}
