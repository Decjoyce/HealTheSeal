using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MedicineTwo : MonoBehaviour
{
    public Image sealImage;
    public Color[] tColour;

    public Transform resetPos;

    public float lookTime_max;
    public float lookTime_min;

    public float backAmount;

    bool can_move = true;
    bool is_moving;
    [SerializeField] float speed, scale_change;

    RectTransform rectTransform;

    [SerializeField] Animator anim;

    [SerializeField] MinigameManager mg_manager;

    bool gameover;

    bool reached_fish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Look());
    }

    private IEnumerator Look()
    {
        if (reached_fish)
            yield break;

        // Not Looking
        sealImage.color = tColour[2];
        yield return new WaitForSecondsRealtime(Random.Range(lookTime_min, lookTime_max));
        //Telegraph
        sealImage.color = tColour[1];

        yield return new WaitForSecondsRealtime(Random.Range(lookTime_min, lookTime_max));
        //Looking
        sealImage.color = tColour[0];
        if (is_moving)
        {
            can_move = false;
            transform.position = resetPos.position;
            sealImage.transform.localScale = Vector3.one * 0.7f;
        }
        yield return new WaitForSecondsRealtime(Random.Range(lookTime_min, lookTime_max));
        //No more looking
        can_move = true;
        StartCoroutine(Look());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) && can_move)
        {
            rectTransform.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);
            sealImage.transform.localScale += Vector3.one * scale_change * Time.deltaTime;
            is_moving = true;
        }
        else
        {
            is_moving = false;
        }


        if(rectTransform.anchoredPosition.y <= 50 && !reached_fish)
        {
            is_moving = false;
            reached_fish = true;
            anim.SetBool("FeedPill", true);
            can_move = false;
        }
    }
}
