using UnityEngine;

public class SnuffleBoard : MonoBehaviour
{
    //public Vector3[] pos; 
    public GameObject fishPrefab;

    [SerializeField] MinigameManager mg_manager;

    void Start()
    {
        int max = Random.Range(4, 8);
        switch (mg_manager.current_difficulty)
        {
            case 1:
                max = Random.Range(6, 9);
                Debug.LogWarning("HighDifficulty");
                break;
            case 2:
                Debug.LogWarning("MEDDIFFICAULY");
                max = Random.Range(2, 6);
                break;
            case 3:
                Debug.LogWarning("LowDifficulty");
                max = Random.Range(1, 3);
                break;
        }

        mg_manager.ChangeWinScore(max);
        Debug.LogWarning(max);
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int spawn = Random.Range(0, 2);
                int i = (x * 3) + y;
                if ((spawn == 0 && max >0)|| (max+i) >=9)
                {
                    Debug.LogWarning("POP");
                    Vector3 pos = new Vector3(150 * x, 150 * y, 0);
                    pos = transform.localPosition + pos;
                    GameObject fish = Instantiate(fishPrefab, transform.position, transform.rotation, transform.parent);
                    fish.transform.localPosition = pos;
                    int dir = Random.Range(0, 2);
                    if (dir == 0)
                    {
                        fish.GetComponent<FishMove>().direction = false;
                    }
                    else
                    {
                        fish.GetComponent<FishMove>().direction = true;
                    }
                    max--;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            Destroy(collision.gameObject);
            mg_manager.IncreaseScore(1);
        }
    }

}
