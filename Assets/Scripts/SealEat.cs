using TMPro;
using UnityEngine;

public class SealEat : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationAngle;

    public GameObject seal;
    [SerializeField] MinigameManager mg_manager;

    [SerializeField] TextMeshProUGUI score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score.text = new string("Fish Ate:\n" + mg_manager.score + " / " + mg_manager.win_score);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed*Time.deltaTime, Space.Self);
        rotationAngle = transform.eulerAngles.z;
        if(rotationAngle >= 30 && rotationAngle <= 330 || rotationAngle <= 330 && rotationAngle >= 30)
        {
            rotationSpeed = -rotationSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            Destroy(collision.gameObject);
            mg_manager.IncreaseScore(1);
            score.text = new string("Fish Ate:\n" + mg_manager.score + " / " + mg_manager.win_score);
            // seal.GetComponent<SealStats>().FeedSeal(5);
        }
    }
}
