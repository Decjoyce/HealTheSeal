using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SealBehaviour : MonoBehaviour
{
    public Seal sealData;
    TextMeshPro text_name; //Prototype

    public Vector2 lowHPPosition;    // for HP: 0–30
    public Vector2 midHPPosition;    // for HP: 31–60
    public Vector2 highHPPosition;   // for HP: 61–100

    private int previousHealth;

    private void Start()
    {
        text_name = GetComponentInChildren<TextMeshPro>(); //Prototype
        text_name.text = sealData.seal_name; //Prototype

        previousHealth = (int)sealData.health;
        UpdateSealPosition();

    }

    public void SetSealData(Seal seal)
    {
        sealData = seal;
        transform.position = seal.position;
    }

    void OnMouseDown()
    {
       if (sealData.health >= 90f && sealData.weight >= 40f)
        {
            // Seal ready to release!
            SealManager.Instance.selectedSeal = sealData;
            SceneManager.LoadScene("Release Scene");
        }
        else
        {
            // Normal flow to detailed view
            SealManager.Instance.selectedSeal = sealData;
            SceneManager.LoadScene("SealDetailScene");
        }
    }
    void UpdateSealPosition()
    {
        if (sealData.health <= 30)
            transform.position = lowHPPosition;
        else if (sealData.health <= 60)
            transform.position = midHPPosition;
        else
            transform.position = highHPPosition;
    }

    void Update()
    {

        if (previousHealth != (int)sealData.health)
        {
            previousHealth = (int)sealData.health;
            UpdateSealPosition();
        }


        // Simple random movement
        transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * Time.deltaTime;
        sealData.position = transform.position;
    }
}
