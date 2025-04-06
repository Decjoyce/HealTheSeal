using UnityEngine;

public class Main : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    public static void LoadMain()
    {
        GameObject Main = Instantiate(Resources.Load("Main") as GameObject);
        DontDestroyOnLoad(Main);
    }
}
