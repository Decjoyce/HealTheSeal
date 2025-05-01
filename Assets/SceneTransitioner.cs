using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner instance;
    
    public Animator[] transition_anims;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("ERROR: More than one instance of SCENETRANSITIONER found!");
            return;
        }
        instance = this;
    }
}
