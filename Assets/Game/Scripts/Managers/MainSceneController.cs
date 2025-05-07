using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainSceneController : MonoBehaviour
{
    public GameObject sealPrefab;
    public Button spawnButton;

    [SerializeField] GameObject rescue_text;

    //Button sprites
    public Sprite normalSprite;
    public Sprite alertSprite;

    public Transform main_canvas, other_canvas;

    [SerializeField] Transform[] icu_zones, kennel_zones, pool_zones;

    int num_in_icu, num_in_kennels, num_in_pool;

    [SerializeField] GameObject zones, zone_icu, zone_kennel;
    static bool icu_open, kennels_open;


    [SerializeField] CameraScroll camera_scroll;

    void Start()
    {
        spawnButton.onClick.AddListener(() => GameManagement.instance.LoadScene("Beach_Scene")); //Prototype - changed from just spawning to moving to beach sceneM

        num_in_icu = 0;
        num_in_kennels = 0;
        num_in_pool = 0;

        StartCoroutine(SealAvailabilityTimer());

        if(kennels_open || icu_open)
        {
            zones.SetActive(true);
            zone_icu.SetActive(icu_open);
            zone_kennel.SetActive(kennels_open);
        }

        // Recreate previously existing seals
        foreach (Seal seal in SealManager.Instance.seals)
        {
            SpawnSealVisual(seal);
        }
    }
    void Update()
    {
        if (!SealManager.Instance.isSealAvailableForRescue)
        {
            //spawnButton.GetComponent<Image>().sprite = normalSprite;
        }
    }
    
    void SpawnSeal()
    {
        SealManager.Instance.SpawnSeal();
        Seal newSeal = SealManager.Instance.seals[SealManager.Instance.seals.Count - 1];
        SpawnSealVisual(newSeal);
    }

    void SpawnSealVisual(Seal sealData)
    {
        GameObject sealObj = Instantiate(sealPrefab, main_canvas);
        SealBehaviour sb = sealObj.GetComponent<SealBehaviour>();

        if (sb != null)
        {
            sb.SetSealData(sealData);
            sb.UpdateSealGraphics();
        }
        else
        {
            Debug.LogError("SealBehaviour is missing on prefab!");
        }

        if (sb.sealData.hunger <= 30)
        {
            sealObj.transform.SetParent(icu_zones[num_in_icu], false);
            sealObj.transform.localPosition = Vector2.zero + (Vector2.up * -42f);
            sealObj.transform.localScale = Vector3.one * 4.25f;
            num_in_icu++;
            //Debug.Log(sealObj.transform.position + " / " + icu_zones[num_in_icu].position);
        }
        else if (sb.sealData.hunger > 30 && sb.sealData.hunger <= 70)
        {
            sealObj.transform.SetParent(kennel_zones[num_in_kennels]);
            sealObj.transform.localPosition = Vector2.zero + (Vector2.up * -42f);
            sealObj.transform.localScale = Vector3.one * 4f;
            num_in_kennels++;
        }
        else
        {
            sealObj.transform.position = pool_zones[num_in_pool].position;
            num_in_pool++;
            //Debug.Log(sealObj.transform.position + " / " + icu_zones[num_in_pool].position);
        }
    }

    IEnumerator SealAvailabilityTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 20f));
            SealManager.Instance.SetSealAvailable(true);
            //spawnButton.GetComponent<Image>().sprite = alertSprite;

            rescue_text.SetActive(true);

            // Optionally make button flash or animate here
        }
    }

    public void OpenEnclosure(bool kennels)
    {
        zones.SetActive(true);
        zone_kennel.SetActive(kennels);
        zone_icu.SetActive(!kennels);
        camera_scroll.enabled = false;
        kennels_open = kennels;
        icu_open = !kennels;
    }
    public void CloseEnclosure()
    {
        zones.SetActive(false);
        zone_kennel.SetActive(false);
        zone_icu.SetActive(false);
        camera_scroll.enabled = true;
        kennels_open = false;
        icu_open = false;
    }
}
