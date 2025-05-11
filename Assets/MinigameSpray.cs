using UnityEngine;
using UnityEngine.UI;

public class MinigameSpray : MonoBehaviour
{

    [SerializeField] Image seal_sprite;

    [SerializeField] Color seal_spray_color;
    [SerializeField] SprayCan spray_can;
    int sprayed_area;
    int max_sprayed_area = 10;
    bool gameover;

    [SerializeField] MinigameManager mg_manager;

    [SerializeField] Transform point_01, point_02;

    [SerializeField] float move_speed;

    bool reverse_dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (mg_manager.current_difficulty)
        {
            case 1:
                move_speed = 4;
                break;
            case 2:
                move_speed = 2;
                break;
            case 3:
                move_speed = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1f);
        sprayed_area = cols.Length;
        if (sprayed_area >= max_sprayed_area && !gameover)
        {
            Debug.Log("YO");
            seal_sprite.color = seal_spray_color;
            mg_manager.IncreaseScore(1);
            gameover = true;
        }

        transform.position -= Vector3.up * move_speed * Time.deltaTime;

            if (transform.position.y >= point_01.position.y && reverse_dir)
        {
            reverse_dir = false;
            move_speed = -move_speed;
        }
        if (transform.position.y <= point_02.position.y && !reverse_dir)
        {
            reverse_dir = true;
            move_speed = -move_speed;
        }
    }
}
