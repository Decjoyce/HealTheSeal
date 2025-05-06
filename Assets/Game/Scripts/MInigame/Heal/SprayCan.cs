using UnityEngine;

public class SprayCan : MonoBehaviour
{
    public GameObject _sprayPrefab;

    public float sprayRate;
    float sprayTime;

    Vector3 mousePos;
    Vector3 mousePos2;

    public bool spray;

    [SerializeField] bool for_mg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (for_mg)
        {
            mousePos = Input.mousePosition;
            mousePos.x = mousePos.x - Screen.width / 2;
            mousePos.y = mousePos.y - Screen.height / 2;
            mousePos2 = mousePos / GetComponentInParent<Canvas>().scaleFactor;
            transform.localPosition = mousePos2;
        }
        if (sprayTime <= Time.time && spray)
        {
            if (Input.GetMouseButton(0))
            {
                Quaternion spawnRotation = Quaternion.Euler(0, 0, (Random.Range(0, 359f)));
                GameObject _spray = Instantiate(_sprayPrefab, transform.position, spawnRotation, transform.parent);
                sprayTime = Time.time + sprayRate;
            }
        }
        if(mousePos2.y >= 250f)
        {
            spray = false;
        }
        else if (mousePos2.y <= 250f)
        {
            spray = true;
        }
    }
}
