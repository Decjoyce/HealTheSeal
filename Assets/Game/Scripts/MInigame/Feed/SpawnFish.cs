using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fishPrefab;
    public float thrust = 1f;

    public Vector3 thrustDir;

    public Vector3 mouseDir;
    public Vector3 lastMousePos;
    public float t = 0;
    public float tt = 0;

    void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            t++;
        }


        if (Input.GetMouseButtonUp(0))
        {
            tt = t;
            mouseDir = Input.mousePosition - lastMousePos;
            thrustDir = mouseDir;
            thrustDir = thrustDir / t;
            GameObject fish = Instantiate(fishPrefab, transform.position, transform.rotation, transform);
            fish.GetComponent<Rigidbody2D>().AddForce(thrustDir * thrust, ForceMode2D.Impulse);
            t = 0;
        }
    }
}
