using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SealBehaviour : MonoBehaviour
{
    public Seal sealData;
    TextMeshPro text_name; //Prototype

    private void Start()
    {
        text_name = GetComponentInChildren<TextMeshPro>(); //Prototype
        text_name.text = sealData.seal_name; //Prototype
    }

    public void SetSealData(Seal seal)
    {
        sealData = seal;
        transform.position = seal.position;
    }

    void OnMouseDown()
    {
        SealManager.Instance.selectedSeal = sealData;
        SceneManager.LoadScene("SealDetailScene");
    }

    void Update()
    {
        // Simple random movement
        transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * Time.deltaTime;
        sealData.position = transform.position;
    }
}
