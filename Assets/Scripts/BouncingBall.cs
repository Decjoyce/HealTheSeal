using UnityEngine;
using System.Collections;
using TMPro;

public class BouncingBall : MonoBehaviour
{

    public Collider2D seal;
    public float thrust = 1f;
    public Rigidbody2D ball;
    
    public TextMeshProUGUI score2;

    bool ball_inside, scored;

    public GameObject sealObject;

    public MinigameManager mg_manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seal = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(ball_inside && !scored)
                Bounce();
        }
        score2.text = new string("Bounces:\n" + mg_manager.score + " / " + mg_manager.win_score);
        
    }

    private void Bounce()
    {
        mg_manager.IncreaseScore(1);
        float vel_y = -ball.linearVelocity.y;
        ball.linearVelocity = new Vector3(0, 0, 0);
        float mult = (float) mg_manager.score / 10f;
        ball.AddForce(transform.up * (thrust + mult), ForceMode2D.Impulse);
        ball.gravityScale = 1 + mult;
        scored = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            ball_inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ball_inside = false;
            scored = false;
        }
    }
}
