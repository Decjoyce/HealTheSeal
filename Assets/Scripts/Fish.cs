using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
    public Collider2D fishCol;
    private Vector3 scaleChange;

    public float waitTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scaleChange = new Vector3(-0.05f, -0.05f, -0.05f);
        fishCol = GetComponent<Collider2D>();
        fishCol.enabled = false;
        StartCoroutine(WaitForCol(waitTime));
    }


    private IEnumerator WaitForCol(float waitTime)
    {
        for (int i = 0; i < waitTime; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale += scaleChange;
            Debug.Log("w");
        }
        fishCol.enabled = true;
        while (true) 
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale += scaleChange;
            Debug.Log("w");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Seal")
        {
            Destroy(gameObject);
        }
    }
}
