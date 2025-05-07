using TMPro;
using UnityEngine;

public class SealEat : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationAngle;

    public GameObject seal;
    [SerializeField] MinigameManager mg_manager;

    [SerializeField] TextMeshProUGUI score;

    Animator anim;
    [SerializeField] Animator score_anim;

    AudioSource source;
    [SerializeField] AudioSource score_source;

    [SerializeField] AudioClip seal_chomp, score_sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score.text = ""; //Prototype
        anim = GetComponent<Animator>(); //Prototype
        source = GetComponent<AudioSource>(); //Prototype
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed*Time.deltaTime, Space.Self);
        rotationAngle = transform.eulerAngles.z;
        if(rotationAngle >= 30 && rotationAngle <= 330 || rotationAngle <= 330 && rotationAngle >= 30)
        {
            rotationSpeed = -rotationSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            Destroy(collision.gameObject);
            mg_manager.IncreaseScore(1); //Prototype
            score.text = mg_manager.score.ToString(); //Prototype
            score.transform.localScale = Vector3.one * ExtensionMethods.Map(mg_manager.score, 0, mg_manager.win_score, 20, 60);
            if(mg_manager.score >= mg_manager.win_score)
                score_anim.Play("score_end");
            else
                score_anim.Play("score_tween"); //Prototype
            score_source.pitch = ExtensionMethods.Map(mg_manager.score, 0, mg_manager.win_score, 0.6f, 1.5f);
            score_source.PlayOneShot(score_sound);

            // Seal Anim & Stuff
            anim.Play("animer"); //Prototype
            source.pitch = Random.Range(0.8f, 1.25f);
            source.PlayOneShot(seal_chomp);
        }
    }
}
