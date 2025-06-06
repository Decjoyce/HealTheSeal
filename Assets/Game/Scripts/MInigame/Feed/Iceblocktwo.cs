using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Iceblocktwo : MonoBehaviour
{
    public float thrust;

    public Color c;

    public float sChange;

    Vector3 scaleChange;

    BoxCollider2D col;

    public GameObject ice;
    [SerializeField] Image ice_sprite;

    [SerializeField] Sprite[] g_iceblock;

    Rigidbody2D rb2D;

    [SerializeField] MinigameManager mg_manager;
    bool gameover;

    Vector2 input_dir;
    Vector2 delayed_velocity;

    [SerializeField] AudioSource audio_source;

    [SerializeField] Transform graphics;

    [SerializeField] float rot_speed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!GameManagement.instance.is_pc)
            Input.gyro.enabled = true;

        ice_sprite = ice.GetComponent<Image>();
        c = ice.GetComponent<Image>().color;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();

        switch (mg_manager.current_difficulty)
        {
            case 1:
                sChange = -0.01f;
                break;
            case 2:
                sChange = -0.025f;
                break;
            case 3:
                sChange = -0.05f;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.instance.is_pc)
        {
            float dirH = Input.GetAxis("Horizontal");
            float dirV = Input.GetAxis("Vertical");

            input_dir = new Vector2(dirH, dirV);
        }
        else
        {
            input_dir = Input.gyro.gravity;
        }
        

        float mapped_scale = ExtensionMethods.Map(ice.transform.localScale.x, 0.5f, 1f, 0, 1);

        if(mapped_scale >= 0.75f)
        {
            ice_sprite.sprite = g_iceblock[0];
        }
        else if (mapped_scale < 0.75f && mapped_scale >= 0.50)
        {
            ice_sprite.rectTransform.sizeDelta = new Vector2(330f, 220f);
            ice_sprite.sprite = g_iceblock[1];
        }
        else if (mapped_scale < 0.50f && mapped_scale >= 0.25f)
        {
            ice_sprite.rectTransform.sizeDelta = new Vector2(310f, 160f);
            ice_sprite.sprite = g_iceblock[2];
        }
        else if (mapped_scale < 0.25f && mapped_scale > 0)
        {
            ice_sprite.rectTransform.sizeDelta = new Vector2(330f, 160f);
            ice_sprite.sprite = g_iceblock[3];
        }
        else
        {
            ice_sprite.gameObject.SetActive(false);
        }

        if (rb2D.linearVelocity != Vector2.zero)
        {
            float rotation_angle = Mathf.Atan2(input_dir.normalized.x, -input_dir.normalized.y) * Mathf.Rad2Deg;
            float lerped_angle = Mathf.LerpAngle(graphics.eulerAngles.z, rotation_angle, rot_speed * Time.deltaTime);
            
            //graphics.eulerAngles = Vector3.forward * lerped_angle;
        }
    }

    private void FixedUpdate()
    {
        if (!gameover)
        {
            rb2D.AddForce(input_dir * thrust * Time.fixedDeltaTime, ForceMode2D.Force);

            float sC = sChange * rb2D.linearVelocity.magnitude * Time.fixedDeltaTime;
            Vector2 scaleChange2D = new Vector2(sC, sC);
            scaleChange = new Vector3(sC, sC, sC);

            if (rb2D.linearVelocity.magnitude > 0)
            {
                ice.transform.localScale += scaleChange;
                col.size += scaleChange2D * 100;
                rb2D.linearDamping += sC;
                c.a += sC / 5;
                ice.GetComponent<Image>().color = c;
            }
            if (ice.transform.localScale.x <= 0.5f)
            {
                mg_manager.IncreaseScore(1);
                gameover = true;
                rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
                Input.gyro.enabled = false;
            }
        }
        
    }

    private void LateUpdate()
    {
        delayed_velocity = rb2D.linearVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.AddForce(-delayed_velocity * 0.5f, ForceMode2D.Impulse);
        audio_source.PlayOneShot(audio_source.clip);
    }
}
