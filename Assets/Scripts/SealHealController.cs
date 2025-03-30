using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SealHealController : MonoBehaviour
{
    public int res;
    public Image sealImage;
    public Color tColour;
    public bool sprayable;
    public GameObject nettingPrefab;
    public Vector2 size;
    public int spawnCount;

    public GameObject seal;

    public GameObject[] netting;

    int cCount;

    void Start()
    {
        netting = new GameObject[spawnCount];
        cCount = transform.childCount;
        sealImage = GetComponent<Image>();
        size = GetComponent<RectTransform>().sizeDelta;
        float x = size.x / 2 - 20;
        float y = size.y / 2 - 20;
        for (int i = 0; i < spawnCount; i++)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 0, (Random.Range(0, 359f)));
            GameObject net = Instantiate(nettingPrefab, transform.position, spawnRotation, transform);
            Vector3 spawnPos = new Vector3((Random.Range(-x, x)), (Random.Range(-y, y)), 0);
            net.transform.localPosition = spawnPos;
            netting[i] = net;
        }
    }

    void Update()
    {
        if (cCount == transform.childCount)
        {
            sprayable = true;
        }
        if (sprayable)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1f);
            res = cols.Length;
            if (res >= 12)
            {
                sealImage.color = tColour;
                seal.GetComponent<SealStats>().IncreaseHealth(25);
            }
        }
    }

    public void CanCut()
    {
        for(int i = 0;i < spawnCount;i++)
        {
            if(netting[i] != null)
            {
                netting[i].GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void CannotCut()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            if (netting[i] != null)
            {
                netting[i].GetComponent<Collider2D>().enabled = false;
            }
        }
    }

}
