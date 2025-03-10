using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fishPrefab;
    public float thrust = 1f;

    public Vector3 thrustDir;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            
            GameObject fish = Instantiate(fishPrefab, transform.position, transform.rotation);
            fish.GetComponent<Rigidbody2D>().AddForce(thrustDir * thrust, ForceMode2D.Impulse);

        }
    }
}
