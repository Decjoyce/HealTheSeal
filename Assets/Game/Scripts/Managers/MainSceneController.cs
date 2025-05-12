using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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
    [SerializeField] TextMeshProUGUI[] icu_nametags, kennel_nametags;

    int num_in_icu, num_in_kennels, num_in_pool;

    [SerializeField] GameObject zones, zone_icu, zone_kennel;
    static bool icu_open, kennels_open;


    [SerializeField] CameraScroll camera_scroll;


    bool seal_is_rescuable;
    [SerializeField] Animator alarm_system;

    [SerializeField] GameObject info_icu, info_kennel, info_habitat;

    [SerializeField] TutorialHabitat tutsy;

    void Start()
    {
        spawnButton.onClick.AddListener(() => BeachButtonActivate()); //Prototype - changed from just spawning to moving to beach sceneM

        num_in_icu = 0;
        num_in_kennels = 0;
        num_in_pool = 0;

        if(kennels_open || icu_open)
        {
            zones.SetActive(true);
            camera_scroll.enabled = false;
            zone_icu.SetActive(icu_open);
            zone_kennel.SetActive(kennels_open);
        }

        if (kennels_open)
            GameManagement.instance.current_info = info_kennel;
        if (icu_open)
            GameManagement.instance.current_info = info_icu;
        if (!icu_open && !kennels_open)
            GameManagement.instance.current_info = info_habitat;

        // Recreate previously existing seals
        foreach (Seal seal in SealManager.Instance.seals)
        {
            SpawnSealVisual(seal);
        }
    }

    public void BeachButtonActivate()
    {
        GameManagement.instance.LoadScene("Beach_Scene");
        if (GameManagement.instance.tutorial)
            GameManagement.instance.tutorial_index++;
    }

    void Update()
    {
        alarm_system.SetBool("alarm", SealManager.Instance.isSealAvailableForRescue);
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
            icu_nametags[num_in_icu].text = sb.sealData.seal_name;
            sealObj.transform.localPosition = Vector2.zero + (Vector2.up * -85f);
            sealObj.transform.localScale = Vector3.one * 8.25f;
            sb.text_name.gameObject.SetActive(false);
            num_in_icu++;
            //Debug.Log(sealObj.transform.position + " / " + icu_zones[num_in_icu].position);
        }
        else if (sb.sealData.hunger > 30 && sb.sealData.hunger <= 70)
        {
            sealObj.transform.SetParent(kennel_zones[num_in_kennels]);
            kennel_nametags[num_in_kennels].text = sb.sealData.seal_name;
            sealObj.transform.localPosition = Vector2.zero + (Vector2.up * -120f);
            sealObj.transform.localScale = Vector3.one * 6f;
            num_in_kennels++;
            sb.text_name.gameObject.SetActive(false);
        }
        else
        {
            sealObj.transform.position = pool_zones[num_in_pool].position;
            num_in_pool++;
            sb.text_name.gameObject.SetActive(true);

            //Debug.Log(sealObj.transform.position + " / " + icu_zones[num_in_pool].position);
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
        if (GameManagement.instance.tutorial && !kennels)
            tutsy.ClickedOnICU();
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

    public void OpenLinkToWebsite()
    {
        Application.OpenURL("https://www.sealrescueireland.org/");
    }
}
