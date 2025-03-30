using UnityEngine;
using System.Collections;

public class Spray : MonoBehaviour
{

    Rigidbody2D rb;
    public int res;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        res = cols.Length;
        if (res >= 6)
        {
            Destroy(gameObject);
        }

    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Seal")
        {
            rb.linearDamping = 50f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Seal")
        {
            rb.linearDamping = 0.5f;
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
