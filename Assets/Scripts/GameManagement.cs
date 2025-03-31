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

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
