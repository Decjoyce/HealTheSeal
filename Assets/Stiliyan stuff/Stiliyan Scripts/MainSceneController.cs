using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public GameObject sealPrefab;
    public Button spawnButton;

    void Start()
    {
        spawnButton.onClick.AddListener(() => SceneManager.LoadScene("Beach_Scene"));

        // Recreate previously existing seals
        foreach (Seal seal in SealManager.Instance.seals)
        {
            SpawnSealVisual(seal);
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
