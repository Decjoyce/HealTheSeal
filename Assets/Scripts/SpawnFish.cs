using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fishPrefab;
    public float thrust = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        Debug.Log(pos);
        if (Input.GetKeyDown("space"))
        {
            
            GameObject fish = Instantiate(fishPrefab, transform.position, transform.rotation, transform);
            fish.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);

        }
    }
}
