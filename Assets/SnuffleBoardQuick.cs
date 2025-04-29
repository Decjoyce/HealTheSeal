using UnityEngine;

public class SnuffleBoardQuick : MonoBehaviour
{
    [SerializeField] MinigameManager mg_manager;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            Destroy(collision.gameObject);
            mg_manager.IncreaseScore(1);
        }
    }
}
