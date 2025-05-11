using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SealBehaviour : MonoBehaviour
{
    public Seal sealData;
    public TextMeshProUGUI text_name; //Prototype

    public Vector2 lowHPPosition;    // for HP: 0–30
    public Vector2 midHPPosition;    // for HP: 31–60
    public Vector2 highHPPosition;   // for HP: 61–100

    private int previousHealth;

    private void Start()
    {
        //text_name = GetComponentInChildren<TextMeshProUGUI>(); //Prototype
        text_name.text = sealData.seal_name; //Prototype

        previousHealth = (int)sealData.health;
        //UpdateSealPosition();
        UpdateSealGraphics();
    }

    public void SetSealData(Seal seal)
    {
        sealData = seal;
        transform.position = seal.position;
    }

    void OnMouseDown()
    {
        Debug.Log("Hey");
       if (sealData.health >= 100f && sealData.hunger >= 100f)
        {
            // Seal ready to release!
            SealManager.Instance.selectedSeal = sealData;
            GameManagement.instance.LoadScene("Release Scene");
        }
        else
        {
            // Normal flow to detailed view
            SealManager.Instance.selectedSeal = sealData;
            GameManagement.instance.LoadScene("SealDetailScene");
        }
    }

    public void SealClicked()
    {
        Debug.Log("Hey2");
        if (sealData.health >= 100f && sealData.hunger >= 100f)
        {
            // Seal ready to release!
            SealManager.Instance.selectedSeal = sealData;
            GameManagement.instance.LoadScene("Release Scene");
        }
        else
        {
            // Normal flow to detailed view
            SealManager.Instance.selectedSeal = sealData;
            GameManagement.instance.LoadScene("SealDetailScene");
        }
    }

    void UpdateSealPosition()
    {

    }

    public void UpdateSealGraphics()
    {
        Image sr = GetComponent<Image>();

        if (sealData.hunger <= 30)
        {
            sr.sprite = SealManager.Instance.seal_stuff.g_small_seal_normal;
            //GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
            //transform.localScale = Vector3.one * 4f;
        }
        else if(sealData.hunger > 30 && sealData.hunger <= 70)
        {
            sr.sprite = SealManager.Instance.seal_stuff.g_medium_seal_normal;
            //transform.localScale = Vector3.one * 4.75f;
        }
        else
        {
            sr.sprite = SealManager.Instance.seal_stuff.g_big_seal_water;
            GetComponent<RectTransform>().sizeDelta = new Vector2(48, 32);
            //transform.localScale = Vector3.one * 5.5f;
        }
        Material m = sr.material;
        // m.mainTexture = sr.sprite.texture;
        //Debug.Log(sealData.colour_scheme.primary);
        m.SetColor("_newPrimary", sealData.colour_scheme.primary);
        m.SetColor("_newSecondary", sealData.colour_scheme.secondary);
        m.SetColor("_newTertiary", sealData.colour_scheme.tertiary);
        m.SetColor("_newQuatiary", sealData.colour_scheme.quatiary);
    }

    void Update()
    {

        if (previousHealth != (int)sealData.health)
        {
            previousHealth = (int)sealData.health;
            UpdateSealPosition();
        }


        // Simple random movement
        //transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * Time.deltaTime;
        sealData.position = transform.position;
    }
}
