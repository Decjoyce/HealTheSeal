using UnityEngine;

public class IceBall : MonoBehaviour
{
    public float thrust;

    public float sChange;

    Vector3 scaleChange;

    public GameObject ice;

    Rigidbody2D rb2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float dirH = Input.GetAxis("Horizontal");
        float dirV = Input.GetAxis("Vertical");

        Vector3 thrustDir = new Vector3(dirH, dirV);

        rb2D.AddForce(thrustDir * thrust, ForceMode2D.Force);

        float sC = sChange * rb2D.linearVelocity.magnitude;

        scaleChange = new Vector3(sC, sC, sC);

        if(rb2D.linearVelocity.magnitude > 0)
        {
            ice.transform.localScale += scaleChange;
        }
        if(ice.transform.localScale.x <= 0f)
        {
            this.enabled = false;
        }

    }
}
