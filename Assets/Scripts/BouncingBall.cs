using UnityEngine;

public class BouncingBall : MonoBehaviour
{

    public Collider2D seal;
    public float thrust = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seal = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            seal.enabled = true;  
        }
        else
        {
            seal.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject ball = col.gameObject;
        if (ball.tag == "Ball")
        {
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
            Debug.Log("h");
        }
    }
}
