using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SealHealController : MonoBehaviour
{
    public int res;
    public Image sealImage;
    public Color tColour;
    public bool sprayable;
    public GameObject[] injuryPrefabs;
    public Vector2 size;
    public int spawnCount;

    bool pettable;
    bool sprayed;
    bool seal_healed;

    public GameObject seal;

    public GameObject[] netting;

    public int injury;

    public Transform[] injuryPos;

    int cCount;

    [SerializeField] MinigameManager mg_minigame;

    [SerializeField] Sprite[] no_net_seal;

    [SerializeField] GameObject tools, sealnamer;

    [SerializeField] SO_SealStuff seal_stuff;

    void Start()
    {
        sealImage = GetComponent<Image>();
        cCount = transform.childCount;
        sprayed = false;

        injury = SealManager.Instance.currentSealInjury;

        sealImage.sprite = seal_stuff.GetInjuryGraphics(injury);

        if(injury == 1)
        {
            SpawnNet();
        }
        else
        {
            SpawnHarm();
        }
    }

    void SpawnHarm()
    {
        netting = new GameObject[spawnCount];
        GameObject net = Instantiate(injuryPrefabs[injury], transform.position, transform.rotation, transform);
        net.transform.localPosition = injuryPos[injury].localPosition;
        netting[0] = net;
    }

    void SpawnNet()
    {
        netting = new GameObject[spawnCount];
        size = GetComponent<RectTransform>().sizeDelta;
        float x = size.x / 2 - 20;
        float y = size.y / 2 - 20;
        for (int i = 0; i < spawnCount; i++)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 0, (Random.Range(0, 359f)));
            GameObject net = Instantiate(injuryPrefabs[0], transform.position, spawnRotation, transform);
            Vector3 spawnPos = new Vector3((Random.Range(-x, x)), (Random.Range(-y, y)), 0);
            net.transform.localPosition = spawnPos;
            netting[i] = net;
        }
    }

    void Update()
    {
        if (cCount == transform.childCount && !sprayable) //Prototype
        {
            sealImage.sprite = no_net_seal[0];
            //mg_minigame.IncreaseScore(1); //Prototype
            if(injury == 3)
            {
                pettable = true;
            }
            else
            {
                sprayable = true;
            }
        }
        if (sprayable)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1f);
            res = cols.Length;
            if (res >= 10 && !sprayed)
            {
                sealImage.color = tColour;
                //seal.GetComponent<SealStats>().IncreaseHealth(25);//don't know why this sets health to 100 // there was nothing to tell it to stop once its reached 12 -> && !sprayed
                //mg_minigame.IncreaseScore(1);
                sprayed = true;
            }
        }
    }

    public void CanPet()
    {
        if(sprayed)
        {
            pettable = true;
        }
    }

    public void CannotPet()
    {
        if (sprayed)
        {
            pettable = false;
        }
    }

    void OnMouseDown()
    {
        if (pettable && !seal_healed) //Prototype
        {
            //mg_minigame.IncreaseScore(3); //Prototype
            //GameManagement.instance.LoadHabitatScene();
            tools.SetActive(false);
            sealnamer.SetActive(true);
            seal_healed = true; //Prototype
            //should probably be an animation here aswell
        }
    }

    public void CanCut(int _injury)
    {
        if(_injury == injury)
        {
            for (int i = 0; i < netting.Length; i++)
            {
                if (netting[i] != null)
                {
                    netting[i].GetComponent<Collider2D>().enabled = true;
                }
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
