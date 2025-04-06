using UnityEngine;

public class Main : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    public static void LoadMain()
    {
        GameObject Main = GameObject.Instantiate(Resources.Load("Main") as GameObject);
        GameObject.DontDestroyOnLoad(Main);
    }
}
