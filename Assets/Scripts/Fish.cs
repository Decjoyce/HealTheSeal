using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
    public Collider2D fishCol;

    public float waitTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishCol = GetComponent<Collider2D>();
        fishCol.enabled = false;
        StartCoroutine(WaitForCol(waitTime));
    }


    private IEnumerator WaitForCol(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fishCol.enabled = true;
        print("WaitAndPrint " + Time.time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Seal")
        {
            Destroy(gameObject);
        }
    }
}
