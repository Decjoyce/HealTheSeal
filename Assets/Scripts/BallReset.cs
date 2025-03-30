using UnityEngine;

public class BallReset : MonoBehaviour
{
    public Transform bReset;
    public GameObject bScore;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject ball = col.gameObject;
        if (ball.tag == "Ball")
        {
            ball.transform.position = bReset.position;
            ball.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, 0, 0);
            bScore.GetComponent<BouncingBall>().ResetScore();
        }
    }
}
