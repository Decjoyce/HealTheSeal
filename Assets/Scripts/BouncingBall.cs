using UnityEngine;
using System.Collections;
using TMPro;

public class BouncingBall : MonoBehaviour
{

    public Collider2D seal;
    public float thrust = 1f;
    public GameObject ball_;
    public float waitTime;
    
    public float score_;
    public TMP_Text score2;

    public GameObject sealObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score_ = 0;
        seal = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Bounce(waitTime));
        }
        score2.text = new string("Bounces:\n" + score_);
        if(score_%10 == 0 && score_ > 00)
        {
            sealObject.GetComponent<SealStats>().IncreaseMood(5*(score_/10));
        }
    }

    public void ResetScore()
    {
        score_ = 0;
    }

    private IEnumerator Bounce(float waitTime)
    {
        yield return new WaitForSeconds(waitTime/2);
        seal.enabled = true;
        yield return new WaitForSeconds(waitTime);
        seal.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject ball = col.gameObject;
        if (ball.tag == "Ball")
        {
            score_++;
            ball_ = ball;
            ball.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
        }
    }
}
