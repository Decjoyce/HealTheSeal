using UnityEngine;
using UnityEngine.UI;

public class IceBall : MonoBehaviour
{
    public float thrust;

    public Color c;

    public float sChange;

    Vector3 scaleChange;

    BoxCollider2D col;

    public GameObject ice;

    Rigidbody2D rb2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c = ice.GetComponent<Image>().color;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float dirH = Input.GetAxis("Horizontal");
        float dirV = Input.GetAxis("Vertical");

        Vector3 thrustDir = new Vector3(dirH, dirV);

        rb2D.AddForce(thrustDir * thrust * Time.fixedDeltaTime, ForceMode2D.Force);

        float sC = sChange * rb2D.linearVelocity.magnitude * Time.fixedDeltaTime;
        Vector2 scaleChange2D = new Vector2(sC, sC);
        scaleChange = new Vector3(sC, sC, sC);

        if (rb2D.linearVelocity.magnitude > 0)
        {
            ice.transform.localScale += scaleChange;
            col.size += scaleChange2D * 100;
            rb2D.linearDamping += sC;
            c.a += sC / 5;
            ice.GetComponent<Image>().color = c;
        }
        if (ice.transform.localScale.x <= 0.5f)
        {
            this.enabled = false;
        }

    }
}
