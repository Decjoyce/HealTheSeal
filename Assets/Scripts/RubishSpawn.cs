using UnityEngine;

public class RubishSpawn : MonoBehaviour
{
    public GameObject rubishPrefab;
    public Transform spawn_parent;
    public int spawnCount;

    Vector2 size;
    Vector3 spawnPos;

    public GameObject cl;

    int cCount;

    public bool clean;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clean = false;
        cCount = transform.childCount;
        size = GetComponent<RectTransform>().sizeDelta;
        float x = size.x/2;
        float y = size.y/2;
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject rubbish = Instantiate(rubishPrefab, transform.position, transform.rotation, spawn_parent);
            spawnPos = new Vector3((Random.Range(-x, x)), (Random.Range(-y, y)), 0);
            rubbish.transform.localPosition = spawnPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cCount == transform.childCount)
        {
            clean = true;
            //cl.SetActive(true);
        }
    }
}
