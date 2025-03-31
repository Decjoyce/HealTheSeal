using UnityEngine;

public class BallReset : MonoBehaviour
{
    public Transform bReset;
    public GameObject bScore;

    [SerializeField] MinigameManager mg_manager;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject ball = col.gameObject;
        if (ball.tag == "Ball")
        {
            ball.transform.position = bReset.position;
            ball.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            mg_manager.ResetScore();
        }
    }
}
