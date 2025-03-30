using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
    public Collider2D fishCol;
    private Vector3 scaleChange;

    public float waitTime;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scaleChange = new Vector3(-0.05f, -0.05f, -0.05f);
        fishCol = GetComponent<Collider2D>();
        fishCol.enabled = false;
        StartCoroutine(WaitForCol(waitTime));
    }
    
    void Update()
    {
        if(rb.linearVelocityY <= 0)
        {
            fishCol.enabled = true;
        }
    }


    private IEnumerator WaitForCol(float waitTime)
    {
        while (true) 
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale += scaleChange;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "End")
        {
            Destroy(gameObject);
        }
    }
}
