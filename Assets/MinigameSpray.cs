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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }
    }
}
