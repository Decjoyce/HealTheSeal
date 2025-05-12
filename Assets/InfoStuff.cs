using UnityEngine;

public class InfoStuff : MonoBehaviour
{
    

    public void SetCurrentInfo(GameObject g)
    {
        GameManagement.instance.current_info = g;
    }
}
