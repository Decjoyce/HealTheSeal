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

    public Transform main_canvas;

    [SerializeField] Transform[] icu_zones, kennel_zones, pool_zones;

    int num_in_icu, num_in_kennels, num_in_pool;

    void Start()
    {
        spawnButton.onClick.AddListener(() => SceneManager.LoadScene("Beach_Scene")); //Prototype - changed from just spawning to moving to beach sceneM

        StartCoroutine(SealAvailabilityTimer());

        num_in_icu = 0;
        num_in_kennels = 0;
        num_in_pool = 0;


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
            sealObj.transform.position = icu_zones[num_in_icu].position;
            num_in_icu++;
            Debug.Log(sealObj.transform.position + " / " + icu_zones[num_in_icu].position);
        }
        else if (sb.sealData.hunger > 30 && sb.sealData.hunger <= 70)
        {
            sealObj.transform.position = kennel_zones[num_in_kennels].position;
            num_in_kennels++;
        }
        else
        {
            sealObj.transform.position = pool_zones[num_in_pool].position;
            num_in_pool++;
            Debug.Log(sealObj.transform.position + " / " + icu_zones[num_in_pool].position);
        }
    }
}
