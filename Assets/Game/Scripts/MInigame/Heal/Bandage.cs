using UnityEngine;

public class Bandage : MonoBehaviour
{


    [SerializeField] Sprite bandage_seal;

    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }
}
