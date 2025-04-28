using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainSceneController : MonoBehaviour
{
    public GameObject sealPrefab;
    public Button spawnButton;
    //Button sprites
    public Sprite normalSprite;
    public Sprite alertSprite;

    void Start()
    {
        spawnButton.onClick.AddListener(() => SceneManager.LoadScene("Beach_Scene")); //Prototype - changed from just spawning to moving to beach sceneM

        StartCoroutine(SealAvailabilityTimer());

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
            spawnButton.GetComponent<Image>().sprite = normalSprite;
        }
    }
    IEnumerator SealAvailabilityTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SealManager.Instance.SetSealAvailable(true);
            spawnButton.GetComponent<Image>().sprite = alertSprite;

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
        GameObject sealObj = Instantiate(sealPrefab);
        SealBehaviour sb = sealObj.GetComponent<SealBehaviour>();
        //sb.UpdateSealGraphics();

        if (sb != null)
        {
            sb.SetSealData(sealData);
        }
        else
        {
            Debug.LogError("SealBehaviour is missing on prefab!");
        }
    }
}
